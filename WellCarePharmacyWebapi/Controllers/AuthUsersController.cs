﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WellCarePharmacyWebapi.Business_Logic_Layer.DTO;
using WellCarePharmacyWebapi.Models.Context;
using WellCarePharmacyWebapi.Models.Entities;
using WellCarePharmacyWebapi.Models.Repository.Interfaces;

namespace WellCarePharmacyWebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthUsersController : ControllerBase
    {
        private readonly WellCareDC _repositoryWrapper;
        public IConfiguration _configuration;
        private readonly IRepositoryWrapper _repositoryWrapper1;

        public AuthUsersController(WellCareDC repositoryWrapper,IRepositoryWrapper repositoryWrapper1, IConfiguration configuration)
        {
            _repositoryWrapper = repositoryWrapper;
            _configuration = configuration;
            _repositoryWrapper1 = repositoryWrapper1;
        }


        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                if (loginRequest != null && loginRequest.Email != null && loginRequest.Password != null)
                {

                    var user = await _repositoryWrapper.Users.Include(u => u.Roles).FirstOrDefaultAsync(o => o.Email == loginRequest.Email && o.Password == EncryptPassword(loginRequest.Password));

                    if (user != null)
                    {
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var claims = new[]
                        {
                           new Claim(ClaimTypes.NameIdentifier, user.Name),
                           new Claim(ClaimTypes.Email, user.Email),
                           new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                           new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                        };

                        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.Now.AddMinutes(60),
                            signingCredentials: signIn);
                        var Token = new JwtSecurityTokenHandler().WriteToken(token);
                        var roleid = user.RoleId;
                        var userid = user.Id;
                        var expires = DateTime.Now.AddMinutes(60);
                        return Ok(new { Token, roleid, userid, expires });
                    }
                    return BadRequest("Invalid credentials");
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the login request.");
            }
        }
        [HttpPost("Registration")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequest registration)
        {
            try
            {
                var role = await _repositoryWrapper.Roles.FindAsync(2);

                if (registration == null)
                {
                    return BadRequest(registration);
                }
                var existingUser = await _repositoryWrapper.Users.FirstOrDefaultAsync(u => u.Email == registration.Email);
                if (existingUser != null)
                {
                    return Conflict("Email is already existed");
                }
                User users = new User()
                {
                    Name = registration.Name,
                    Email = registration.Email,
                    PhoneNumber = registration.PhoneNumber,
                    Password = EncryptPassword(registration.Password),
                    RoleId = 2,
                    Roles = role

                };
                await _repositoryWrapper.Users.AddAsync(users);
                await _repositoryWrapper.SaveChangesAsync();
                return Ok("Successfully Registered");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the login request.");
            }

        }
        public static string EncryptPassword(string password)
        {
            byte[] storepassword = ASCIIEncoding.ASCII.GetBytes(password);
            string encryptpassword = Convert.ToBase64String(storepassword);
            return encryptpassword;
        }

    }
}

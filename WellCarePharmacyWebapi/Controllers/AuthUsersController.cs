using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WellCarePharmacyWebapi.Business_Logic_Layer.DTO;
using WellCarePharmacyWebapi.Models.Context;
using WellCarePharmacyWebapi.Models.Entities;




namespace WellCarePharmacyWebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthUsersController : ControllerBase
    {
        private readonly WellCareDC _repositoryWrapper;
        public IConfiguration _configuration;

        public AuthUsersController(WellCareDC repositoryWrapper, IConfiguration configuration)
        {
            _repositoryWrapper = repositoryWrapper;
            _configuration = configuration;
        }

        [AllowAnonymous]
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
                            expires: DateTime.Now.AddMinutes(15),
                            signingCredentials: signIn);

                        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
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
        public async Task<IActionResult> Registration([FromBody] RegistrationRequest registration)
        {
            try
            {
                var role = await _repositoryWrapper.Roles.FindAsync(2);

                if (registration == null)
                {
                    return BadRequest(registration);
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
                //registration.Id = users.Id;
                await _repositoryWrapper.SaveChangesAsync();
                return CreatedAtRoute(nameof(GetAllUsers), new { id = users.Id }, users);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the login request.");
            }
           
        }

        [Authorize(Roles = "1")]
        [HttpGet(Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RegistrationRequest>>> GetAllUsers()
        {
            try
            {

                return Ok(await _repositoryWrapper.Users.Include(u => u.Roles).ToListAsync());
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving users.");
            }
        }

        [HttpDelete("id")]
        [Authorize(Roles = "1")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _repositoryWrapper.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                _repositoryWrapper.Users.Remove(user);
                await _repositoryWrapper.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception)
            {
                return StatusCode(500, "An error occurred while retrieving users.");
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

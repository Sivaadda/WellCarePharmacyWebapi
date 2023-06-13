using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WellCarePharmacyWebapi.Business_Logic_Layer.DTO;
using WellCarePharmacyWebapi.Models.Context;
using WellCarePharmacyWebapi.Models.Entities;
using WellCarePharmacyWebapi.Models.Repository.Interfaces;

namespace WellCarePharmacyWebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IRepositoryWrapper _repositoryWrapper;
        public IConfiguration _configuration;

        public UsersController(IRepositoryWrapper repositoryWrapper, IConfiguration configuration)
        {
            _repositoryWrapper = repositoryWrapper;
            _configuration = configuration;
        }
      

       // [Authorize(Roles = "1")]
        [HttpGet(Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<RegistrationRequest>>> GetAllUsers()
        {
            try
            {

                return Ok(await _repositoryWrapper.Users.GetAllusers());
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving users.");
            }
        }

        [HttpDelete("id")]
       // [Authorize(Roles = "1")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _repositoryWrapper.Users.GetById(id);
                if (user == null)
                {
                    return NotFound();
                }

                await _repositoryWrapper.Users.Delete(id);
                _repositoryWrapper.Save();
                return Ok("User sucessfully delected");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving users.");
            }
        }
       
    }
}

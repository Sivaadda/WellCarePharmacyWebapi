using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WellCarePharmacyWebapi.Business_Logic_Layer.DTO;
using WellCarePharmacyWebapi.Models.Repository.Interfaces;

namespace WellCarePharmacyWebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public OrdersController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<OrdersRequest>> GetAllOrders()
        {
            return Ok(_repositoryWrapper.Orders.GetAll());
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteOrder(int id)
        {
            var order= _repositoryWrapper.Orders.GetById(id);
            if (order == null)
            {
                return NotFound();
            }

            _repositoryWrapper.Orders.Delete(id);
            _repositoryWrapper.Save();
            return NoContent();

        }



    }
}

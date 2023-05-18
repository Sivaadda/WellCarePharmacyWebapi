using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WellCarePharmacyWebapi.Business_Logic_Layer.DTO;
using WellCarePharmacyWebapi.Models.Entities;
using WellCarePharmacyWebapi.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis;

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
        public async Task<ActionResult<IEnumerable<OrdersRespond>>> GetAllOrders()
        {
            return Ok(await _repositoryWrapper.Orders.GetAll());
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order= await _repositoryWrapper.Orders.GetById(id);
            if (order == null)
            {
                return NotFound();
            }

            await _repositoryWrapper.Orders.Delete(id);
            _repositoryWrapper.Save();
            return NoContent();

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrdersRespond>> PostOrder([FromBody] OrdersRespond orders)
        {
            if (orders == null)
            {
                return BadRequest(orders);
            }


            Orders order = new Orders
            {
                Quantity = orders.Quantity,
                TotalPrice = orders.TotalPrice,
                ProductId=orders.ProductId,
                UsersId=orders.UsersId,

               

            };
            await _repositoryWrapper.Orders.Create(order);
            orders.Id = order.Id;
            _repositoryWrapper.Save();
            return CreatedAtRoute("GetOrder", new { id = order.Id }, order);


        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id", Name = "GetOrder")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var value = await _repositoryWrapper.Orders.GetById(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateOrders(int id, [FromBody] OrdersRespond orders)
        {
            var orderid = await _repositoryWrapper.Orders.GetById(orders.Id);
            if (orders == null)
            {
                return NotFound();
            }

            orderid.Quantity = orders.Quantity;
            orderid.TotalPrice = orders.TotalPrice;
            orderid.ProductId = orders.ProductId;
            orderid.UsersId = orders.UsersId;
           
        
            return Ok(orderid);

        }

    }
}

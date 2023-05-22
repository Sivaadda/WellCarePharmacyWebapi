using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WellCarePharmacyWebapi.Business_Logic_Layer.DTO;
using WellCarePharmacyWebapi.Models.Entities;
using WellCarePharmacyWebapi.Models.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using System.Text;

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


        [HttpGet("GetAllOrders")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrdersRespond>>> GetAllOrders()
        {
            try
            {
                return Ok(await _repositoryWrapper.Orders.GetAll());

            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the login request.");
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "2")]
        [HttpGet("id", Name = "GetOrder")]
        public async Task<IActionResult> GetOrder(int id)
        {
            try
            {
                var value = await _repositoryWrapper.Orders.GetById(id);
                if (value == null)
                {
                    return NotFound();
                }
                return Ok(value);

            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the login request.");
            }

        }
        [HttpPost("AddOrder")]
        [Authorize(Roles = "2")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrdersRespond>> PostOrder([FromBody] OrdersRespond orders)
        {

            try
            {
                if (orders == null)
                {
                    return BadRequest(orders);
                }

                Order order = new Order
                {
                    Quantity = orders.Quantity,
                    TotalPrice = orders.TotalPrice,
                    ProductId = orders.ProductId,
                    UsersId = orders.UsersId,

                };
                await _repositoryWrapper.Orders.Create(order);
                orders.Id = order.Id;
                _repositoryWrapper.Save();
                return CreatedAtRoute("GetOrder", new { id = order.Id }, order);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the login request.");
            }
        }

        
        [Authorize(Roles = "2")]
        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateOrders(int id, [FromBody] OrdersRespond orders)
        {
            try
            {
                var orderid = await _repositoryWrapper.Orders.GetById(id);
                if (orderid != null)
                {
                    orderid.Quantity = orders.Quantity;
                    orderid.TotalPrice = orders.TotalPrice;
                    orderid.ProductId = orders.ProductId;
                    orderid.UsersId = orders.UsersId;
                    await _repositoryWrapper.Orders.Update(orderid);
                    _repositoryWrapper.Save();
                    return Ok(orderid);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the login request.");
            }


        }
        [HttpDelete("id")]
        [Authorize(Roles = "2")]
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



    }
}

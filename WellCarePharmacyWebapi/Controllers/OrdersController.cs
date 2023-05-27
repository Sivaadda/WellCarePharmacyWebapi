using Microsoft.AspNetCore.Mvc;
using WellCarePharmacyWebapi.Business_Logic_Layer.DTO;
using WellCarePharmacyWebapi.Models.Entities;
using WellCarePharmacyWebapi.Models.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<ActionResult<IEnumerable<OrdersDTO>>> GetAllOrders()
        {
            try
            {
                return Ok(await _repositoryWrapper.Orders.GetAllorders()) ;

            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while getting all orders.");
            }
        }


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
        public async Task<ActionResult<OrdersDTO>> PostOrder([FromBody] OrdersDTO orders)
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
                _repositoryWrapper.Save();
                return Ok(orders);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the login request.");
            }
        }


        [HttpDelete("id")]
        [Authorize(Roles = "2")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var order = await _repositoryWrapper.Orders.GetById(id);
                if (order == null)
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

                await _repositoryWrapper.Orders.Delete(id);
                _repositoryWrapper.Save();
                return Ok("Order is sucessfully delected");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while delecting Order.");
            }

        }

    }
}

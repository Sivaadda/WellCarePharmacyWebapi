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
        public async Task<ActionResult<IEnumerable<OrdersRequest>>> GetAllOrders()
        {
            try
            {
                return Ok(await _repositoryWrapper.Orders.GetAllorders());

            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while getting all orders.");
            }
        }

        [HttpPost("AddOrder")]
        [Authorize(Roles = "2")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrdersRequest>> PostOrder([FromBody] OrdersRequest orders)
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
                    UsersId = orders.UsersId
                };

                // Retrieve the associated product based on the product ID
                Product product = await _repositoryWrapper.Products.GetById(orders.ProductId);
                if (product == null)
                {
                    return NotFound($"Product with ID {orders.ProductId} not found.");
                }

                // Retrieve the associated user based on the user ID
                User user = await _repositoryWrapper.Users.GetuserById(orders.UsersId);
                if (user == null)
                {
                    return NotFound($"User with ID {orders.UsersId} not found.");
                }

                order.Products = product;
                order.Users = user;

                await _repositoryWrapper.Orders.Create(order);
                _repositoryWrapper.Save();

                // Map the order, product, and user to the OrdersDTO for response
                OrderRespond response = new OrderRespond
                {
                    Id = order.Id,
                    Quantity = order.Quantity,
                    TotalPrice = order.TotalPrice,
                    ProductId = order.ProductId,
                    UsersId = order.UsersId,
                    Products= order.Products,
                    Users= order.Users,

                };

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while adding an order.");
            }

        }

        [HttpDelete("id")]
        [Authorize(Roles = "2")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var order = await _repositoryWrapper.Orders.GetById(id);
                if (order == null)
                {
                    return NotFound();
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
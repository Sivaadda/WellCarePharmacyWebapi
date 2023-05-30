using Microsoft.AspNetCore.Mvc;
using WellCarePharmacyWebapi.Business_Logic_Layer.DTO;
using WellCarePharmacyWebapi.Models.Entities;
using WellCarePharmacyWebapi.Models.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

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
             
                var orders = await _repositoryWrapper.Orders.GetAllorders();
                var orderrequest = orders.Select(p => new OrderRespond
                {
                    Id = p.Id,
                    Quantity = p.Quantity,
                    TotalPrice = p.TotalPrice,
                    Users = p.Users,
                    Products = p.Products.Select(p => new ProductOrderRespond 
                    {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Descripition=p.Descripition,
                    Discount=p.Discount,
                    Status=p.Status


                    }).ToList()
                }).ToList();
                return Ok(orderrequest);

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
                    UsersId = orders.UsersId,
                    Products = new List<Product>()
                };

                User user = await _repositoryWrapper.Users.GetById(orders.UsersId);
                if (user == null)
                {
                    return NotFound($"User with ID {orders.UsersId} not found.");
                }

                foreach (ProductOrderRequest productOrderReq in orders.Products)
                {
                    // Retrieve the associated product based on the product ID
                    Product product = await _repositoryWrapper.Products.GetById(productOrderReq.ProductId);
                    if (product == null)
                    {
                        return NotFound($"Product with ID {productOrderReq.ProductId} not found.");
                    }

                    // Add the product to the order
                    order.Products.Add(product);
                }

                order.Users = user;

                await _repositoryWrapper.Orders.Create(order);
                _repositoryWrapper.Save();

                return Ok("Order is successfully placed");
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
                return Ok("Order is successfully delected");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while delecting Order.");
            }

        }

    }
}
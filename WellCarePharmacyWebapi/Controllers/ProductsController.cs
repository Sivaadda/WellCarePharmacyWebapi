using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WellCarePharmacyWebapi.Business_Logic_Layer.DTO;
using WellCarePharmacyWebapi.Models.Entities;
using WellCarePharmacyWebapi.Models.Repository.Interfaces;

namespace WellCarePharmacyWebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ProductsController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
       
        [HttpGet("GetAllProducts")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
        {

            try
            {
                return Ok(await _repositoryWrapper.Products.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while feching all products.");
            }
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        [HttpGet("id", Name = "GetProduct")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var value = await _repositoryWrapper.Products.GetById(id);
                if (value == null)
                {
                    return NotFound();
                }
                return Ok(value);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while feching product by id.");
            }

        }

        [HttpPost("AddProduct")]
        [Authorize(Roles = "1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ProductDTO>> PostProduct([FromBody] ProductDTO product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest(product);
                }

                Product products = new Product
                {

                    ProductName = product.ProductName,
                    Price = product.Price,
                    Discount = product.Discount,
                    Descripition = product.Descripition,
                    Status = product.Status,
                    ImageUrl = product.ImageUrl,


                };
                await _repositoryWrapper.Products.Create(products);
                _repositoryWrapper.Save();
                return CreatedAtRoute("GetProduct", new { id = products.Id}, products);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating a products.");
            }

        }

        [HttpPut("id")]
        [Authorize(Roles = "1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDTO product)
        {
            try
            {
              
                var productsid = await _repositoryWrapper.Products.GetById(id);
                if (productsid != null)
                {
                    productsid.ProductName = product.ProductName;
                    productsid.Price = product.Price;
                    productsid.Discount = product.Discount;
                    productsid.Descripition = product.Descripition;
                    productsid.ImageUrl = product.ImageUrl;
                    productsid.Status = product.Status;

                    await _repositoryWrapper.Products.Update(productsid);
                    _repositoryWrapper.Save();
                    return Ok(productsid);
                }
              
                return NotFound();

            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating a product.");
            }


        }

        [HttpDelete("id")]
        [Authorize(Roles = "1")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            try
            {
                var product = await _repositoryWrapper.Products.GetById(id);
                if (product == null)
                {
                    return NotFound();
                }

                await _repositoryWrapper.Products.Delete(id);
                _repositoryWrapper.Save();
                return Ok("Product is delected successfully");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while delecting a product.");
            }


        }

    }
}


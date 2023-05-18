using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WellCarePharmacyWebapi.Business_Logic_Layer.DTO;
using WellCarePharmacyWebapi.Models.Context;
using WellCarePharmacyWebapi.Models.Entities;
using WellCarePharmacyWebapi.Models.Repository.Imp;
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductRequest>>> GetAllProducts()
        {
            return Ok(await _repositoryWrapper.Products.GetAll());
        }



        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product=  await _repositoryWrapper.Products.GetById(id);
            if(product == null)
            {
                return NotFound();
            }

            await _repositoryWrapper.Products.Delete(id);
            _repositoryWrapper.Save();
            return NoContent();

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductRequest>> PostProduct([FromBody] ProductRequest product)
        {
            if (product == null)
            {
                return BadRequest(product);
            }
          
            Products products = new Products
            {
               
                ProductName = product.ProductName,
                Price = product.Price,
                Discount = product.Discount,
                Descripition = product.Descripition,
                Status = product.Status,
                ImageUrl = product.ImageUrl,


            };
            await _repositoryWrapper.Products.Create(products);
            product.Id = products.Id;
            _repositoryWrapper.Save();
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);

           
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id" , Name ="GetProduct")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var value = await _repositoryWrapper.Products.GetById(id);
            if(value==null)
            {
                return NotFound();
            }
            return Ok(value);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateProduct(int id,[FromBody]ProductRequest product)
        {
           
          
                var productsid = await _repositoryWrapper.Products.GetById(product.Id);
               if (product == null)
                 {
                return NotFound();
                 }
            productsid.ProductName = product.ProductName;
                productsid.Price = product.Price;
                productsid.Discount = product.Discount;
                productsid.Descripition = product.Descripition;
                productsid.Status = product.Status;
                productsid.ImageUrl = product.ImageUrl;
                await _repositoryWrapper.Products.Update(productsid);

                return Ok(productsid);
                




        }


    }
}


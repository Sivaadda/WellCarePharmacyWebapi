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
        public ActionResult<IEnumerable<ProductRequest>> GetAllProducts()
        {
            return Ok(_repositoryWrapper.Products.GetAll());
        }



        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteProduct(int id)
        {
            var product= _repositoryWrapper.Products.GetById(id);
            if(product == null)
            {
                return NotFound();
            }

            _repositoryWrapper.Products.Delete(id);
            _repositoryWrapper.Save();
            return NoContent();

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProductRequest> PostProduct([FromBody] ProductRequest product)
        {
            if (product == null)
            {
                return NotFound();
            }
            Products products = new Products
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                Discount=product.Discount,
                Descripition=product.Descripition,
                Status = product.Status,
                ImageUrl=product.ImageUrl,

            };

            _repositoryWrapper.Products.Create(products);
            _repositoryWrapper.Save();
            return Ok(products);
        }

      


    }
}


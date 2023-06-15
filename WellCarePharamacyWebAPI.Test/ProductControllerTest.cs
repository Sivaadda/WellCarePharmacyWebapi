using System;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellCarePharmacyWebapi.Models.Repository.Interfaces;
using WellCarePharmacyWebapi.Controllers;
using WellCarePharmacyWebapi.Models.Repository.Imp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WellCarePharmacyWebapi.Business_Logic_Layer.DTO;
using WellCarePharmacyWebapi.Models.Entities;

namespace WellCarePharmacyWebApi.Test
{
    [TestFixture]
    public class ProductControllerTest
    {
        private Mock<IRepositoryWrapper> repositoryWrapperMock;
        private ProductsController productsController;

        [SetUp]
        public void Setup()
        {
            repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            productsController = new ProductsController(repositoryWrapperMock.Object);
        }
        [Test]
        public async Task GetAllProducts_ReturnsOkResultWithProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, ProductName = "Product 1" },
                new Product { Id = 2, ProductName = "Product 2" }
            };
            repositoryWrapperMock.Setup(x => x.Products.GetAll()).ReturnsAsync(products);

            // Act
            var result = await productsController.GetAllProducts();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            var productResponds = okResult.Value as List<ProductRespond>;
            Assert.NotNull(productResponds);
            Assert.AreEqual(products.Count, productResponds.Count);
        }

        [Test]
        public async Task GetAllProducts_ReturnsStatusCode500_WhenRepositoryThrowsException()
        {
            // Arrange
            repositoryWrapperMock.Setup(x => x.Products.GetAll()).Throws(new Exception());

            // Act
            var result = await productsController.GetAllProducts();

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result.Result);
            var objectResult = result.Result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
        }

        [Test]
        public async Task GetProduct_ReturnsOkResultWithProduct()
        {
            // Arrange
            var product = new Product { Id = 1, ProductName = "Product 1" };
            repositoryWrapperMock.Setup(x => x.Products.GetById(It.IsAny<int>())).ReturnsAsync(product);

            // Act
            var result = await productsController.GetProduct(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var productDTO = okResult.Value as ProductRespond;
            Assert.NotNull(productDTO);
            Assert.AreEqual(product.Id, productDTO.Id);
        }

        [Test]
        public async Task GetProduct_ReturnsNotFoundResult_WhenProductNotFound()
        {
            // Arrange
            repositoryWrapperMock.Setup(x => x.Products.GetById(It.IsAny<int>())).ReturnsAsync((Product)null);

            // Act
            var result = await productsController.GetProduct(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task UpdateProduct_ReturnsOkResult_WhenProductExists()
        {
            // Arrange
            int productId = 1;
            var productRequest = new ProductRequest
            {
                
                ProductName = "Updated Product",
                Price = 99,
                Descripition = "Updated Description",
                Discount = 1,
                Status = "Updated",
                ImageUrl = "updated-image.jpg"
            };

            var existingProduct = new Product
            {
                Id = productId,
                ProductName = "Original Product",
                Price = 456,
                Descripition = "Original Description",
                Discount = 2,
                Status = "Original",
                ImageUrl = "original-image.jpg"
            };

            repositoryWrapperMock.Setup(x => x.Products.GetById(productId)).ReturnsAsync(existingProduct);

            // Act
            var result = await productsController.UpdateProduct(productId, productRequest);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual("Product sucessfully updated ", okResult.Value);

            // Verify that the repository method was called with the updated product
            repositoryWrapperMock.Verify(x => x.Products.Update(It.Is<Product>(p =>
                p.Id == productId &&
                p.ProductName == productRequest.ProductName &&
                p.Price == productRequest.Price &&
                p.Descripition == productRequest.Descripition &&
                p.Discount == productRequest.Discount &&
                p.Status == productRequest.Status &&
                p.ImageUrl == productRequest.ImageUrl)), Times.Once);
        }
    }
}


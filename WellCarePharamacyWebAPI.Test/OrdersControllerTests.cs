using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellCarePharmacyWebapi.Business_Logic_Layer.DTO;
using WellCarePharmacyWebapi.Controllers;
using WellCarePharmacyWebapi.Models.Entities;
using WellCarePharmacyWebapi.Models.Repository.Interfaces;

namespace WellCarePharmacyWebApi.Test
{
    [TestFixture]
    public class OrdersControllerTests
    {
        private Mock<IRepositoryWrapper> repositoryWrapperMock;
        private OrdersController ordersController;

        [SetUp]
        public void Setup()
        {
            repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            ordersController = new OrdersController(repositoryWrapperMock.Object);
        }

        [Test]
        public async Task GetOrder_ReturnsOkResult_WhenOrderExists()
        {
            // Arrange
            var orderId = 1;
            var order = new Order { Id = orderId, TotalQuantity = 5 };
            repositoryWrapperMock.Setup(x => x.Orders.Getorder(orderId)).ReturnsAsync(order);

            // Act
            var result = await ordersController.GetOrder(orderId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreEqual(order, okResult.Value);
        }

        [Test]
        public async Task GetOrder_ReturnsNotFoundResult_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderId = 1;
            repositoryWrapperMock.Setup(x => x.Orders.Getorder(orderId)).ReturnsAsync((Order)null);

            // Act
            var result = await ordersController.GetOrder(orderId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
            var notFoundResult = result as NotFoundResult;
            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Test]
        public async Task DeleteOrder_ReturnsOkResult_WhenOrderExists()
        {
            // Arrange
            var orderId = 1;
            repositoryWrapperMock.Setup(x => x.Orders.GetById(orderId)).ReturnsAsync(new Order());

            // Act
            var result = await ordersController.DeleteOrder(orderId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreEqual("Order is successfully deleted", okResult.Value);
        }

        [Test]
        public async Task DeleteOrder_ReturnsNotFoundResult_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderId = 1;
            repositoryWrapperMock.Setup(x => x.Orders.GetById(orderId)).ReturnsAsync((Order)null);

            // Act
            var result = await ordersController.DeleteOrder(orderId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
            var notFoundResult = result as NotFoundResult;
            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

    }
}

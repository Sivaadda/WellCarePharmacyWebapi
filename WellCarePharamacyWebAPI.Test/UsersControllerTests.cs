using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using WellCarePharmacyWebapi.Controllers;
using WellCarePharmacyWebapi.Models.Entities;
using WellCarePharmacyWebapi.Models.Repository.Interfaces;

namespace WellCarePharmacyWebapi.Test
{
    [TestFixture]
    public class UsersControllerTests
    {
        private Mock<IRepositoryWrapper> repositoryWrapperMock;
        private UsersController usersController;

        [SetUp]
        public void Setup()
        {
            repositoryWrapperMock = new Mock<IRepositoryWrapper>();
            usersController = new UsersController(repositoryWrapperMock.Object, null);
        }

        [Test]
        public async Task GetAllUsers_ReturnsOkResult()
        {
            // Arrange
            var users = new List<User> { new User { Id = 1, Name = "User 1" }, new User { Id = 2, Name = "User 2" } };
            repositoryWrapperMock.Setup(x => x.Users.GetAllusers()).ReturnsAsync(users);

            // Act
            var result = await usersController.GetAllUsers();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            var userList = okResult.Value as List<User>;
            Assert.AreEqual(2, userList.Count);
            // Additional assertions as needed
        }

        [Test]
        public async Task DeleteUser_ReturnsOkResult_WhenUserExists()
        {
            // Arrange
            int userId = 1;
            repositoryWrapperMock.Setup(x => x.Users.GetById(userId)).ReturnsAsync(new User { Id = userId });

            // Act
            var result = await usersController.DeleteUser(userId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.AreEqual("User sucessfully delected", okResult.Value);
        }

        [Test]
        public async Task DeleteUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            int userId = 1;
            repositoryWrapperMock.Setup(x => x.Users.GetById(userId)).ReturnsAsync(null as User);

            // Act
            var result = await usersController.DeleteUser(userId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
            var notFoundResult = result as NotFoundResult;
            Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }
    }
}
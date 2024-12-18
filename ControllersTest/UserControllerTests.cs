using BankAPI.Controllers;
using BankAPI.Entities;
using BankAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAPI.Tests.ControllersTest
{
    public class UserControllerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockUserRepo = new Mock<IUserRepository>();
            _controller = new UserController(null, _mockUserRepo.Object);
        }

        [Fact]
        public async Task AddUserAsync_ShouldReturnOKResponse()
        {
            // Arrange
            var nUser = new User
            {
                FirstName = "harry",
                LastName = "Potter",
                PersonalNumber = "09045645649",
                Email = "harry.potter@gmail.com",
                PhoneNumber = "090909"
            };

            _mockUserRepo.Setup(repo => repo.AddUserAsync(It.IsAny<User>())).ReturnsAsync(nUser);
           
            // Act
            var result = await _controller.RegisterUser(nUser);

            // Assert
            var actionResult = Assert.IsType < ActionResult < User >>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<User>(okResult.Value);
            Assert.Equal(nUser.FirstName, returnValue.FirstName);
        }
    }


   
}

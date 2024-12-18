using BankAPI.Controllers;
using BankAPI.Entities;
using BankAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAPI.Tests.ControllersTest
{
    public class AccountControllerTests
    {
        private readonly Mock<IAccountRepository> _mockRepo;
        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            _mockRepo = new Mock<IAccountRepository>();
            _controller = new AccountController(null, _mockRepo.Object);
        }

        [Fact]
        public async Task Deposit_ShouldReturnUpdatedAccountBalance()
        {
            // Arrange
            var account = new Account
            {
                AccountNumber = "123456",
                Balance = 150,
                Status = "Active"
            };

            _mockRepo.Setup(repo => repo.AddDepositAsync(1, 50)).ReturnsAsync(account);

            // Act
            var result = await _controller.Deposit(1, 50) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(account, result.Value);
        }

        [Fact]
        public async Task Withdraw_ShouldReturnUpdatedAccountBalance()
        {
            // Arrange
            var account = new Account
            {
                AccountNumber = "123456",
                Balance = 500,
                Status = "Active"
            };

            _mockRepo.Setup(repo => repo.AddWithdrawAsync(2, 50)).ReturnsAsync(account);

            // Act
            var result = await _controller.Withdraw(2, 50) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(account, result.Value);
        }

        [Fact]
        public async Task GetUserAccountsByID_ShouldReturn_OKResult()
        {
            // Arrange
            var accounts = new List<Account>
            { 
                new Account { ID = 1, AccountNumber = "123456", Balance = 100 },
                new Account { ID = 2, AccountNumber = "789101", Balance = 200 }
            };

            _mockRepo.Setup(repo => repo.GetAccountsAsyncByUserId(1)).ReturnsAsync(accounts);

            // Act
            var result = await _controller.GetUserAccounts(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(accounts, okResult.Value);
        }
    }
}

using BankAPI.Controllers;
using BankAPI.Entities;
using BankAPI.Interfaces;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAPI.Tests.ControllersTest
{
    public class TransactionsControllerTests
    {
        private readonly Mock<ITransactionRepository> _transactionRepo;
        private readonly TransactionController _bankController;

        public TransactionsControllerTests()
        {
            _transactionRepo = new Mock<ITransactionRepository>();
            _bankController = new TransactionController(null, _transactionRepo.Object);
        }

        [Fact]
        public async Task TransactionsByAccIdAsync_ReturnOKResult()
        {
            // Arrange
            var transactions = new List<BankTransactions>
            {
                new BankTransactions { ID = 1, FromAccountID = 558, ToAccountID = 5454, Amount = 100, Status = "active"},
                new BankTransactions { ID = 2, FromAccountID = 488, ToAccountID = 558, Amount = 200, Status = "active"}
            };

            _transactionRepo.Setup(repo => repo.GetTransactionsByAccIdAsync(4004)).ReturnsAsync(transactions);

            // Act
            var result = await _bankController.GetTransactionHistory(4004);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);

            var OKResult = result.Result as OkObjectResult;
            Assert.NotNull(OKResult);
            Assert.Equal(200, OKResult.StatusCode);
            Assert.Equal(transactions, OKResult.Value); 
           
        }
    }
}

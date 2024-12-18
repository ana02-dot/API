using BankAPI.Data;
using BankAPI.Entities;
using BankAPI.Interfaces;
using BankAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankAPI.Tests.RepositoriesTest
{
    public class TransactionRepositoryTests
    {
        private readonly DataContext _context;
        private readonly ITransactionRepository _transactionRepo;

        public TransactionRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;
            _context = new DataContext(options);
            _transactionRepo = new TransactionsRepository(_context);
        }


        [Fact]
        public async Task GetTransactionsByAccIdAsync_Return_Transactions_By_AccountID()
        {
            // Arrange
            var transactions = new List<BankTransactions>
            {
                new BankTransactions { ID = 1, FromAccountID = 558, ToAccountID = 5454, Amount = 100, Status = "active"},
                new BankTransactions { ID = 2, FromAccountID = 488, ToAccountID = 558, Amount = 200, Status = "active"}
            };

            await _context.BankTransactions.AddRangeAsync(transactions);
            await _context.SaveChangesAsync();

            // Act
            var result = await _transactionRepo.GetTransactionsByAccIdAsync(558);

            // Assert
            Assert.NotNull(result);
           
            Assert.IsType<List<BankTransactions>>(result);
            Assert.Equal(2, result.Count); 
            Assert.Equal(558, result[0].FromAccountID);
            Assert.Contains(result, t => t.FromAccountID == 558); 
            Assert.Contains(result, t => t.ToAccountID == 558);
        }

    }
}

using AutoFixture;
using BankAPI.Data;
using BankAPI.Entities;
using BankAPI.Interfaces;
using BankAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAPI.Tests.RepositoriesTest
{
    public class AccountRepositoryTests
    {
        private readonly DataContext _dataContext;
        private readonly AccountRepository _repository;

        public AccountRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
            _dataContext = new DataContext(options);
            _repository = new AccountRepository(_dataContext);
        }


        [Fact]
        public async Task OpenAccountAsync_ShouldCreateNewAccountForExistingUser()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                PersonalNumber = "12345678901",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890"
            };

            var account = new Account
            {
                AccountNumber = "789101",
                Balance = 0,
                Currency = "USD",
                AccountType = "Savings",
                UserID = user.Id 
            };

            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();

            // Act
            var createdAccount = await _repository.OpenAccountAsync(account);

            // Assert
            Assert.NotNull(createdAccount);
            Assert.Equal("Active", createdAccount.Status);
            Assert.Equal(user.Id, createdAccount.UserID);
            Assert.Equal(account.AccountNumber, createdAccount.AccountNumber);
        }
    }
}

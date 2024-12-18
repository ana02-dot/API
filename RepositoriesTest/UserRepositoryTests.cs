using BankAPI.Data;
using BankAPI.Entities;
using BankAPI.Interfaces;
using BankAPI.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAPI.Tests.RepositoriesTest
{
    public class UserRepositoryTests
    {
        private readonly DataContext _dataContext;
        private readonly IUserRepository _repo;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _dataContext = new DataContext(options);
            _repo = new UserRepository(_dataContext);
        }

        [Fact]
        public async Task AddUserAsync_Should_AddNewUsers()
        { 
            //Arrange
            var nUser = new User
            {
                FirstName = "harry",
                LastName = "Potter",
                PersonalNumber = "09045645649",
                Email = "harry.potter@gmail.com",
                PhoneNumber = "090909"
            };


            // Act
            var createdUser = await _repo.AddUserAsync(nUser);

            // Assert
            Assert.NotNull(createdUser);
            Assert.Equal("harry", createdUser.FirstName);
            Assert.Equal("Potter", createdUser.LastName);
            Assert.Equal("harry.potter@gmail.com", createdUser.Email);

        }
    }
}

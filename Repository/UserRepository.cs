using BankAPI.Data;
using BankAPI.Entities;
using BankAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext; 
        }
        public async Task<User> AddUserAsync(User user)
        {
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();
            return new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PersonalNumber = user.PersonalNumber,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }
    }
}

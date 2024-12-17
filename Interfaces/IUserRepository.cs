using BankAPI.Entities;

namespace BankAPI.Interfaces
{
    public interface IUserRepository
    {
        Task <User> AddUserAsync(User user);   

    }
}

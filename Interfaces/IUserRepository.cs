using BankAPI.Entities;

namespace BankAPI.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);   

    }
}

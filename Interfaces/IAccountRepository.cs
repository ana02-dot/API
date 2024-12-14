using BankAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Interfaces
{
    public interface IAccountRepository
    {
        Task OpenAccountAsync(Account account);
        Task<Account?> AddDepositAsync(int accountId, decimal amount);
        Task<List<Account>> GetAccountsAsyncByUserId(int userId);
        Task<Account?> AddWithdrawAsync(int id, decimal amount);
    }
}

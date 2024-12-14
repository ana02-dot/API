using BankAPI.Data;
using BankAPI.Entities;
using BankAPI.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BankAPI.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _dataContext;
        public AccountRepository(DataContext dataContext)
        {
            _dataContext = dataContext;

        }

        public async Task<Account?> AddDepositAsync(int id, decimal amount)
        {
            var account = await _dataContext.Accounts.FindAsync(id);

            // Return null if the account doesn't exist
            if (account == null) return null;

            // Update the balance
            account.Balance += amount;

            // Save changes to the database
            _dataContext.Accounts.Update(account);
            await _dataContext.SaveChangesAsync();

            return account;

        }

       

        public async Task<Account?> AddWithdrawAsync(int id, decimal amount)
        {
            var account = await _dataContext.Accounts.FindAsync(id);

            if (account == null) 
                return null;

            if (account.Balance < amount)
                Console.WriteLine("Insufficient balance");

            account.Balance -= amount;
            
            _dataContext.Accounts.Update(account);
            await _dataContext.SaveChangesAsync();
            return account;
        }

       
        public async Task <List<Account>> GetAccountsAsyncByUserId(int userId)
        {
            return await _dataContext.Accounts
                       .Where(a => a.UserID == userId)
                       .ToListAsync();
        }


        public async Task OpenAccountAsync(Account account)
        {
            account.CreatedDate = DateTime.Now;
            account.Status = "Active";
            _dataContext.Accounts.Add(account);
            await _dataContext.SaveChangesAsync();
        }
    }
}

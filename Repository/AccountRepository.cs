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
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be greater than zero.");

            using var transaction = await _dataContext.Database.BeginTransactionAsync();
            try
            {
                var account = await _dataContext.Accounts.FindAsync(id)
                              ?? throw new KeyNotFoundException("Account not found.");

                if (account.Status != "Active")
                    throw new InvalidOperationException("Account is not active.");

                account.Balance += amount;

                _dataContext.Accounts.Update(account);
                await _dataContext.SaveChangesAsync();

                await transaction.CommitAsync();
                return account;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

        }



        public async Task<Account?> AddWithdrawAsync(int id, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be greater than zero.");

            
            using var transaction = await _dataContext.Database.BeginTransactionAsync();
            try
            {
                var account = await _dataContext.Accounts.FindAsync(id)
                              ?? throw new KeyNotFoundException("Account not found.");

                if (account.Status != "Active")
                    throw new InvalidOperationException("Account is not active.");

                if (account.Balance < amount)
                    throw new InvalidOperationException("Insufficient balance.");

                account.Balance -= amount;

                _dataContext.Accounts.Update(account);
                await _dataContext.SaveChangesAsync();

                await transaction.CommitAsync();
                return account;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw; 
            }

        }


        public async Task<List<Account>> GetAccountsAsyncByUserId(int userId)
        {
            var accounts = await _dataContext.Accounts
                               .Where(a => a.UserID == userId)
                               .ToListAsync();

            return accounts.Any()
                ? accounts
                : throw new KeyNotFoundException("No accounts found for the user.");
           

        }


        public async Task<Account> OpenAccountAsync(Account account)
        {
            if (account == null)
                throw new ArgumentException("Account data is required.");

            var user = await _dataContext.Users.FindAsync(account.UserID)
                       ?? throw new KeyNotFoundException("User not found.");

            account.CreatedDate = DateTime.UtcNow;
            account.Status = "Active";

            _dataContext.Accounts.Add(account);
            await _dataContext.SaveChangesAsync();

            return new Account
            {
                ID = account.ID,
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,
                Currency = account.Currency,
                AccountType = account.AccountType,
                UserID = account.UserID,
                CreatedDate = account.CreatedDate,
                Status = account.Status
            };
        }
    }
}

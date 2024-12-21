using BankAPI.Data;
using BankAPI.Entities;
using BankAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BankAPI.Repository

{
    public class TransactionsRepository :ITransactionRepository
    {
        private readonly DataContext _dataContext;
        private readonly ICurrencyConverter _currencyConverter;
        public TransactionsRepository(DataContext dataContext, ICurrencyConverter currencyConverter)
        {
            _dataContext = dataContext;
            _currencyConverter = currencyConverter;
        }

        public async  Task <string> TransferAsync(BankTransactions BankTransactions)
        {
            if (BankTransactions.Amount <= 0)
                throw new ArgumentException("Transfer amount must be greater than zero.");

            using var dbTransaction = await _dataContext.Database.BeginTransactionAsync();
            try
            {
                var fromAccount = await _dataContext.Accounts.FindAsync(BankTransactions.FromAccountID)
                ?? throw new KeyNotFoundException("Source account not found.");

                var toAccount = await _dataContext.Accounts.FindAsync(BankTransactions.ToAccountID)
                                ?? throw new KeyNotFoundException("Target account not found.");

                if (fromAccount.Status != "Active" || toAccount.Status != "Active")
                    throw new InvalidOperationException("Both accounts must be active.");

                if (fromAccount.Balance < BankTransactions.Amount)
                    throw new InvalidOperationException("Insufficient balance in the source account.");



                decimal convertedAmount = BankTransactions.Amount;
                if (fromAccount.Currency != toAccount.Currency)
                {
                    convertedAmount = _currencyConverter.Convert(BankTransactions.Amount, fromAccount.Currency, toAccount.Currency);
                }



                // Update balances
                fromAccount.Balance -= BankTransactions.Amount;
                toAccount.Balance += BankTransactions.Amount;

                // Add transaction record
                BankTransactions.TransactionDate = DateTime.UtcNow;
                BankTransactions.Status = "Completed";
                _dataContext.BankTransactions.Add(BankTransactions);

                await _dataContext.SaveChangesAsync();
                await dbTransaction.CommitAsync();

                return "Transfer completed successfully.";
            }
            catch
            {
                await dbTransaction.RollbackAsync();
                throw;
            }
        }

        

        async Task<List<BankTransactions>> ITransactionRepository.GetTransactionsByAccIdAsync(int accountId)
        {
            return await _dataContext.BankTransactions
                            .Where(t => t.FromAccountID == accountId || t.ToAccountID == accountId)
                            .OrderByDescending(t => t.TransactionDate)
                            .ToListAsync();
        }


    }
}

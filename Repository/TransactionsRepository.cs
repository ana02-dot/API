using BankAPI.Data;
using BankAPI.Entities;
using BankAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using Transaction = BankAPI.Entities.Transaction;

namespace BankAPI.Repository

{
    public class TransactionsRepository : ITransactionRepository
    {

        private readonly DataContext _dataContext;



        public TransactionsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Transaction>> GetTransactionsByAccIdAsync(int accountId)
        {

            return await _dataContext.Transactions
                .Where(t => t.FromAccountID == accountId || t.ToAccountID == accountId)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }

        
    }
}

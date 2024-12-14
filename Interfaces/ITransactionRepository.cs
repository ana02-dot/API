using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace BankAPI.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetTransactionsByAccIdAsync(int accountId);
    }
}

using BankAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace BankAPI.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<BankTransactions>> GetTransactionsByAccIdAsync(int accountId);
        Task<string> TransferAsync(BankTransactions transaction);
    }
}

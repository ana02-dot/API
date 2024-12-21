using BankAPI.Data;
using BankAPI.Entities;
using BankAPI.Interfaces;
using BankAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Transactions;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
       private readonly DataContext _dataContext;
        private readonly ITransactionRepository _transactionRepo;
        public TransactionController(DataContext dataContext, ITransactionRepository transactionRepo)

        {
            _dataContext = dataContext;
            _transactionRepo = transactionRepo;
        }


        /// <summary>
        /// Gets transaction history for a specific account.
        /// </summary>
        /// <param name="accountId">The ID of the account to fetch transactions for.</param>
        /// <returns>A list of transactions.</returns>

        [HttpGet("account/{accountId}")]
        public async Task<ActionResult<List<BankTransactions>>> GetTransactionHistory(int accountId)
        {
            
            var transactions = await _transactionRepo.GetTransactionsByAccIdAsync(accountId);

            if (transactions == null || transactions.Count == 0)
            {
                throw new KeyNotFoundException("No transactions found for this account.");
            }

            return Ok(transactions);
        }

        /// <summary>
        /// Transfers an amount of money between two accounts.
        /// </summary>
        /// <param name="transaction">The transaction details.</param>
        /// <returns>A success message or error details.</returns>
        [HttpPost("transfer")]
        public async Task<ActionResult> Transfer([FromBody] BankTransactions transaction)
        {
            var result = await _transactionRepo.TransferAsync(transaction);

            return Ok(new { message = result });
        }



    } 
}

using BankAPI.Data;
using BankAPI.Entities;
using BankAPI.Interfaces;
using BankAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Transactions;
using Transaction = BankAPI.Entities.Transaction;

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


        [HttpGet("account/{accountId}")]
        public async Task<ActionResult<List<Transaction>>> GetTransactionHistory(int accountId)
        {
            try
            {
                var transactions = await _transactionRepo.GetTransactionsByAccIdAsync(accountId);

                if (transactions == null || transactions.Count == 0)
                {
                    return NotFound("No transactions found for this account.");
                }

                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            
            
        }
        
           
           
    } 
}

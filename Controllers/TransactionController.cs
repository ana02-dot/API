using BankAPI.Data;
using BankAPI.Entities;
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

        public TransactionController(DataContext _Datacontext)
        {
             _dataContext = _Datacontext;
        }


        [HttpGet("account/{accountId}")]
        public async Task<ActionResult<List<Transaction>>> GetTransactionHistory(int accountId)
        {
            var transactions = await _dataContext.Transactions
                .Where(t => t.FromAccountID == accountId || t.ToAccountID == accountId)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
            return Ok(transactions);
        }
        
           
           
    } 
}

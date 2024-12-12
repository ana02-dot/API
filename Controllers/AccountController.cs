using BankAPI.Data;
using BankAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public AccountController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost("open")]
        public async Task<ActionResult<Account>> OpenAccount([FromBody] Account account)
        {
            account.CreatedDate = DateTime.Now;
            account.Status = "Active";
            _dataContext.Accounts.Add(account);
            await _dataContext.SaveChangesAsync();
            return Ok(account);
        }

        [HttpPost("{id}/deposit")]
        public async Task<ActionResult> Deposit(int id, [FromBody] decimal amount)
        {
            var account = await _dataContext.Accounts.FindAsync(id);
            if (account == null) return NotFound("Account not found");

            account.Balance += amount;
            await _dataContext.SaveChangesAsync();
            return Ok(account);
        }

        [HttpPost("{id}/withdraw")]
        public async Task<ActionResult> Withdraw(int id, [FromBody] decimal amount)
        {
            var account = await _dataContext.Accounts.FindAsync(id);
            if (account == null) return NotFound("Account not found");

            if (account.Balance < amount) return BadRequest("Insufficient balance");

            account.Balance -= amount;
            await _dataContext.SaveChangesAsync();
            return Ok(account);
        }


        [HttpPost("transfer")]
        public async Task<ActionResult> Transfer([FromBody] Transaction transaction)
        {
            var fromAccount = await _dataContext.Accounts.FindAsync(transaction.FromAccountID);
            var toAccount = await _dataContext.Accounts.FindAsync(transaction.ToAccountID);

            if (fromAccount == null || toAccount == null)
                return NotFound("One or both accounts not found");

            if (fromAccount.Balance < transaction.Amount)
                return BadRequest("Insufficient balance");

            fromAccount.Balance -= transaction.Amount;
            toAccount.Balance += transaction.Amount;

            transaction.TransactionDate = DateTime.UtcNow;
            transaction.Status = "Completed";

            _dataContext.Transactions.Add(transaction);
            await _dataContext.SaveChangesAsync();

            return Ok("Transfer successful");
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Account>>> GetUserAccounts(int userId)
        {
            var accounts = await _dataContext.Accounts.Where(a => a.UserID == userId).ToListAsync();
            return Ok(accounts);
        }

    }
}

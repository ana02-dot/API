using BankAPI.Data;
using BankAPI.Entities;
using BankAPI.Interfaces;
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
        private readonly IAccountRepository _accountRepo;

        public AccountController(DataContext dataContext, IAccountRepository accountRepo)
        {
            _dataContext = dataContext;
            _accountRepo = accountRepo;
        }

        [HttpPost("openAccount")]
        public async Task<ActionResult<Account>> OpenAccount([FromBody] Account account)
        {
            var newAccount = await _accountRepo.OpenAccountAsync(account);
            return Ok(newAccount);
            
        }
                      

        [HttpPost("{id}/deposit")]
        public async Task<ActionResult> Deposit(int id, [FromBody] decimal amount)
        {
                var account = await _accountRepo.AddDepositAsync(id, amount);
                return Ok(account);
            
        }

        [HttpPost("{id}/withdraw")]
        public async Task<ActionResult> Withdraw(int id, [FromBody] decimal amount)
        {
                var account = await _accountRepo.AddWithdrawAsync(id, amount);
                return Ok(account);
        }


        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Account>>> GetUserAccounts(int userId)
        {
            var accounts = await _accountRepo.GetAccountsAsyncByUserId(userId);

            if (accounts == null || accounts.Count == 0)
            {
                throw new KeyNotFoundException($"No accounts found for user ID {userId}.");
            }

            return Ok(accounts);

        }

    }
}

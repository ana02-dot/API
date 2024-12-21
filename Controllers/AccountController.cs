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

        /// <summary>
        /// Opens a new account for an existing user.
        /// </summary>
        /// <param name="account">The account details to be created.</param>
        /// <returns>The newly created account.</returns>
        [HttpPost("openAccount")]
        public async Task<ActionResult<Account>> OpenAccount([FromBody] Account account)
        {
            var newAccount = await _accountRepo.OpenAccountAsync(account);
            return Ok(newAccount);
            
        }

        /// <summary>
        /// Deposits money into an account.
        /// </summary>
        /// <param name="id">The ID of the account to deposit money into.</param>
        /// <param name="amount">The amount of money to deposit.</param>
        /// <returns>The updated account details after the deposit.</returns>
        [HttpPost("{id}/deposit")]
        public async Task<ActionResult> Deposit(int id, [FromBody] decimal amount)
        {
                var account = await _accountRepo.AddDepositAsync(id, amount);
                return Ok(account);
            
        }

        /// <summary>
        /// Withdraws money from an account.
        /// </summary>
        /// <param name="id">The ID of the account to withdraw money from.</param>
        /// <param name="amount">The amount of money to withdraw.</param>
        /// <returns>The updated account details after the withdrawal.</returns>
        [HttpPost("{id}/withdraw")]
        public async Task<ActionResult> Withdraw(int id, [FromBody] decimal amount)
        {
                var account = await _accountRepo.AddWithdrawAsync(id, amount);
                return Ok(account);
        }



        /// <summary>
        /// Gets all accounts associated with a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user to fetch accounts for.</param>
        /// <returns>A list of accounts belonging to the user.</returns>
        
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

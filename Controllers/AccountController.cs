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

        [HttpPost("open")]
        public async Task<ActionResult<Account>> OpenAccount([FromBody] Account account)
        {
            await _accountRepo.OpenAccountAsync(account);
            return Ok(account);
        }

        [HttpPost("{id}/deposit")]
        public async Task<ActionResult> Deposit(int id, [FromBody] decimal amount)
        {
            try
            {
                if (amount <= 0)
                {
                    return BadRequest("Deposit amount must be greater than zero.");
                }

                // Call repository method to update the account balance
                var account = await _accountRepo.AddDepositAsync(id, amount);

                if (account == null)
                {
                    return NotFound($"Account with ID {id} not found.");
                }

                return Ok(account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("{id}/withdraw")]
        public async Task<ActionResult> Withdraw(int id, [FromBody] decimal amount)
        {
            try
            {
                if (amount <= 0)
                {
                    return BadRequest("Deposit amount must be greater than zero.");
                }

                var account = await _accountRepo.AddWithdrawAsync(id, amount);

                if (account == null)
                {
                    return NotFound($"Account with ID {id} not found.");
                }

                return Ok(account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
          
        }


       

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Account>>> GetUserAccounts(int userId)
        {
            try
            {
                var accounts = await _accountRepo.GetAccountsAsyncByUserId(userId);

                if (accounts == null || accounts.Count == 0)
                {
                    return NotFound($"No accounts found for user ID {userId}.");
                }

                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}

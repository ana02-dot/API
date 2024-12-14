using BankAPI.Data;
using BankAPI.Entities;
using BankAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IUserRepository _userRepo;
        public UserController(DataContext dataContext, IUserRepository userRepo)
        {
            _userRepo = userRepo;
            _dataContext = dataContext;
        }


        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser([FromBody] User user)
        {
            await _userRepo.AddUserAsync(user);
            return Ok(user);
        }
    }
}

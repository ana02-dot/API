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


        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user">Details of the user to register.</param>
        /// <returns>The created user.</returns>
        /// 
        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser([FromBody] User user)
        {
            var registeredUser = await _userRepo.AddUserAsync(user);
            return Ok(registeredUser);
            
        }
    }
}

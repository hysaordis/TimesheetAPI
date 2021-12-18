using Microsoft.AspNetCore.Mvc;
using TimesheetAPI.Services.Interfaces;
using TimesheetAPI.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TimesheetAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _authenticationService;

        public UserController(IUserService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ApiAuthenticateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authenticationService.Authenticate(model);
                if (result == null)
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] ApiAuthenticateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authenticationService.Register(model);
                if (result == null)
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // create endpoint for get list of users
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _authenticationService.GetUsers();
            return Ok(users);
        }

        // create endpoint for get user by name
        [HttpGet("user/{userName}")]
        public async Task<IActionResult> GetUserByName(string userName)
        {
            var user = await _authenticationService.GetUserByName(userName);
            return Ok(user);
        }

        // create endpoint for update ApplicationUser in database
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] ApiAuthenticateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authenticationService.Update(model);
                if (result == null)
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // create endpoint for delete ApplicationUser in database
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] ApiAuthenticateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authenticationService.Delete(model);
                if (result == null)
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Nadmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]UserDto userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.UserName) || string.IsNullOrWhiteSpace(userDto.Password))
                return BadRequest(new { msg = "userName or password is null" });

            if (userDto.Password != userDto.ConfirmPassword)
                return BadRequest(new { msg = "Passwords don't match!" });

            var newUser = new IdentityUser
            {
                UserName = userDto.UserName,
                //Email = userName
            };

            IdentityResult userCreationResult = null;
            try
            {
                userCreationResult = await _userManager.CreateAsync(newUser, userDto.Password);
            }
            catch (SqlException)
            {
                return StatusCode(500, new { msg = "Error communicating with the database, see logs for more details" });
            }

            if (!userCreationResult.Succeeded)
            {
                return BadRequest(new
                {
                    msg = "An error occurred when creating the user, see nested errors",
                    errors =
                        userCreationResult.Errors.Select(x => new
                        {
                            msg = $"[{x.Code}] {x.Description}"
                        })
                });
            }

            return Ok(new { msg = "Registration completed" });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserDto userDto)
        {
            if (string.IsNullOrWhiteSpace(userDto.UserName) || string.IsNullOrWhiteSpace(userDto.Password))
                return BadRequest(new { msg = "userName or password is null" });

            var user = await _userManager.FindByNameAsync(userDto.UserName);
            if (user == null)
                return BadRequest(new { msg = "Invalid Login and/or password" });

            var passwordSignInResult = await _signInManager.PasswordSignInAsync(user, userDto.Password, isPersistent: true, lockoutOnFailure: false);
            if (!passwordSignInResult.Succeeded)
                return BadRequest(new { msg = "Invalid Login and/or password" });

            return Ok(new { msg = "Cookie created" });
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok(new { msg = "You have been successfully logged out" });
        }

        public class UserDto
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }
    }
}
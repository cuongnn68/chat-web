using System.Threading.Tasks;
using DiscordRipoff.Entities;
using DiscordRipoff.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiscordRipoff.Controllers {
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller{
        private UserServices userServices;

        public UserController(UserServices userServices) {
            this.userServices = userServices;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model) {
            var user = new User {
                Username = model.Username,
                Password = model.Password,
                FullName = model.FullName,
                Email = model.Email
            };
            var ok = await userServices.CreateUserAsync(user);
            if(ok) return Ok();
            return Conflict("Cant register");
        }

        [HttpPost]
        [Route("auth")]
        public async Task<IActionResult> LoginAsync(LoginModel model) {
            var ok = await userServices.ValidateUserAsync(model.Username, model.Password);
            if(!ok) return Unauthorized("");
            // create JWT and return below
            return Ok();
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterModel {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
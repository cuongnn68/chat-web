using DiscordRipoff.Entities;
using DiscordRipoff.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiscordRipoff.Controllers {
    [ApiController]
    public class UserController {
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
            if(ok) return RedirectToRouteResult;
        }
    }

    public class RegisterModel {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
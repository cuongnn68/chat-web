using System.Linq;
using System.Threading.Tasks;
using DiscordRipoff.Data;
using DiscordRipoff.Entities;
using DiscordRipoff.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiscordRipoff.Controllers {
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller{
        private UserServices userServices;
        private JWTService jWTService;
        private AppDbContext dbContext;

        public UserController(
            UserServices userServices,
            JWTService jWTService,
            AppDbContext dbContext) 
        {
            this.userServices = userServices;
            this.jWTService = jWTService;
            this.dbContext = dbContext;
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
        public async Task<IActionResult> Login(LoginModel model) {
            var ok = await userServices.ValidateUserAsync(model.Username, model.Password);
            if(!ok) return Unauthorized("Wrong password or username");
            var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Username == model.Username);
            var token = jWTService.CreateToken(user.Id, user.Username);
            return Ok(token);
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout(string token) {
            var loggedOut = new JWTBlacklist {
                JWT = token,
                TimeExpired = jWTService.GetTimeExpired(token),
            };
            await dbContext.Blacklist.AddAsync(loggedOut);
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
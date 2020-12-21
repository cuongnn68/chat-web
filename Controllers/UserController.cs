using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DiscordRipoff.Data;
using DiscordRipoff.Entities;
using DiscordRipoff.Services;
using DiscordRipoff.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiscordRipoff.Controllers {
    [ApiController]
    [Route("api/user")]
    // [JWTAuthentication]
    public class UserController : Controller {
        private UserServices userServices;
        private RoomServices roomServices;
        private JWTService jwtService;
        private AppDbContext dbContext;

        public UserController(
            UserServices userServices,
            RoomServices roomServices,
            JWTService jwtService,
            AppDbContext dbContext) {
            this.userServices = userServices;
            this.roomServices = roomServices;
            this.jwtService = jwtService;
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model) {
            Console.WriteLine("register ----");
            var exist = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == model.Username) != null;
            if(exist) return Conflict(new ErrorModel{Error = "Username already exist"});
            var user = new User {
                Username = model.Username.ToLower(),
                Password = model.Password,
                FullName = model.FullName,
                Email = model.Email,
                Phone = model.Phone
            };
            var ok = await userServices.CreateUserAsync(user);
            if (ok) return Ok();
            return Conflict(new ErrorModel{Error = "Cant register"});
        }

        [HttpPost("auth")]
        public async Task<IActionResult> Login(LoginModel model) {
            // throw new Exception("test");
            var ok = await userServices.ValidateUserAsync(model.Username.ToLower(), model.Password);
            if (!ok) return Unauthorized(new ErrorModel{Error="Wrong password or username"});
            var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Username == model.Username);
            var token = jwtService.CreateToken(user.Id, user.Username);
            var userInfo = new UserInfoModel {
                Token = token,
                Username = user.Username,
                Id = user.Id
            };
            return Ok(userInfo);
        }

        [JWTAuthentication]
        [HttpPost("check-auth")]
        public IActionResult CheckAuth() {
            // Console.WriteLine(HttpContext.Request.Headers["KhongCo"]); // if not exist return ""
            var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ").Last();
            var ok = jwtService.ValidateTokent(dbContext, token);
            if (ok) return Ok("Passed");
            return Unauthorized("Not Passed");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] JWTModel model) {
            var loggedOut = new JWTBlacklist {
                JWT = model.Token,
                TimeExpired = jwtService.GetTimeExpired(model.Token),
            };
            await dbContext.Blacklist.AddAsync(loggedOut);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{userId}")]
        [JWTAuthentication]
        public async Task<IActionResult> GetUser(int userId) {
            User user;
            try {
                user = await dbContext.Users.FindAsync(userId);
                if(user == null) throw new NullReferenceException();
            } catch (NullReferenceException) {
                return Conflict(new ErrorModel{Error = "User not exist"});
            } catch (Exception e) {
                Console.WriteLine(e);
                return BadRequest();
            }
            return Ok(user);
        }

        // [HttpPut("{userId}")] //TODO http put user 
        // [JWTAuthentication]
        // public IActionResult UpdateUser(int userId) {
            
        // }

        // TODO http put user/password

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchUserModel model) {
            if (model.Id < 1 && model.Username == null) return Ok(new List<User>());
            Console.WriteLine(model.Id);
            Console.WriteLine(model.Username);
            var users = await dbContext.Users
                .Where(user => user.Id == model.Id || user.Username.Contains(model.Username))
                .ToListAsync();
            return Ok(users);
        }

        [HttpGet("{userId}/room")]
        [JWTAuthentication]
        public async Task<IActionResult> GetRoom(int userId) {
            var rooms = await userServices.GetRoomAsync(userId);
            // var roomModels = rooms.Select(room => new RoomModel {
            //     Id = room.Id,
            //     Name = room.Name,
            //     DayCreated = room.DayCreated
            // }).ToList();
            // foreach(var room in rooms) {
            //     Console.WriteLine(room.Name);
            // }
            return Ok(rooms);
        }

        [HttpPost("{userId}/room")]
        [JWTAuthentication]
        public async Task<IActionResult> CreateRoom(int userId, [FromBody] RoomModel model) { // RM: put data in model so it can be binding
            var room = await roomServices.CreateAsync(model.RoomName, userId);
            if(room == null) return Conflict(new ErrorModel{Error="Cant create room"});
            return Created($"/api/room/{room.Id}",room);
        }
    }

    public class RoomModel {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public DateTime DayCreated { get; set; }
    }

    public class UserInfoModel {
        public string Token { get; set; }
        public int Id { get; set; }
        public string Username { get; set; }
    }

    public class JWTModel {
        public string Token { get; set; }
    }

    public class SearchUserModel {
        public int Id { get; set; }
        public string Username { get; set; }

    }

    public class LoginModel {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class RegisterModel {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class ErrorModel {
        [Required]
        public string Error { get; set; }
    }
}
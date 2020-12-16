using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DiscordRipoff.Data;
using DiscordRipoff.Entities;
using DiscordRipoff.Hubs;
using DiscordRipoff.Services;
using DiscordRipoff.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace DiscordRipoff.Controllers {
    [ApiController]
    [Route("api/room")]
    // [JWTAuthentication]
    public class RoomController : Controller {
        private RoomServices roomServices;
        private AppDbContext dbContext;

        public RoomController (
            RoomServices roomServices,
            AppDbContext dbContext,
            IHubContext<ChatHub> hub
        ) {
            this.roomServices = roomServices;
            this.dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            var room = await dbContext.Rooms.FirstOrDefaultAsync(room => room.Id == id);
            if(room == null) return NotFound();
            return Ok(room);
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewRoomModel model) {
            var room = await roomServices.CreateAsync(model.RoomName, model.UserId);
            return Created($"api/room/{room.Id}",room);
        }

        [HttpPut("{roomId}")]
        public async Task<IActionResult> Update([FromBody] UpdateRoomModel model, int roomId) {
            var ok = await roomServices.UpdateAsync(roomId, model.Name);
            return Ok();
        }

        [HttpDelete("{roomId}")]
        public async Task<IActionResult> Remove(int roomId) {
            Console.WriteLine(roomId);
            var removed = await roomServices.RemoveAsync(roomId);
            if(removed) return Ok();
            return Conflict();
        }

        [HttpGet("{roomId}/user")]
        public async Task<IActionResult> GetUser(int roomId) {
            var users = await roomServices.GetAllUsersAsync(roomId);
            if(users == null) return NotFound();
            return Ok(users);
        }

        [HttpPost("{roomId}/user")]
        public async Task<IActionResult> AddUser(int roomId, [FromBody] NewUserModel model) {
            try {
                var ru = await roomServices.AddUserAsync(roomId, model.UserId);
                if(ru == null) throw new NullReferenceException();
            } catch {
                return Conflict(new ErrorModel{Error="Cant join room"});
            }
            return Ok();
        }

        [HttpPost("{roomId}/user/{userId}")]
        // public async Task<IActionResult> AddUser(int roomId, int userId) {
            //TODO //FIXME model binding not working
        public async Task<IActionResult> AddRoomUser([FromRoute] RoomUserModel model) {
            // Console.WriteLine(roomId + " " + userId);
            // var roomUser = await roomServices.AddUserAsync(roomId, userId);
            Console.WriteLine(model.RoomId + " " + model.UserId);
            var roomUser = await roomServices.AddUserAsync(model.RoomId, model.UserId);
            if(roomUser == null) return Conflict();
            return Created($"/api/room/{roomUser.Id}",roomUser);
        }

        [HttpDelete("{roomId}/user/{userId}")]
        public async Task<IActionResult> RemoveUser(int roomId, int userId) {
        // public async Task<IActionResult> RemoveUser([FromQuery] RoomUserModel model) {
            var removed = await roomServices.RemoveUserAsync(userId, roomId);
            // var removed = await roomServices.RemoveUserAsync(model.UserId, model.RoomId);
            // RM: for real??????????????
            if(removed) return Ok();
            return Conflict();
        }

        // TODO: change user role

        [HttpGet("{roomId}/message")]
        public async Task<IActionResult> GetMessage(int roomId) {
            var mess = await roomServices.GetMessagesAsync(roomId);
            if(mess == null) return Conflict();
            var messModel = mess.Select(mess => new MessageModel{
                                        Id = mess.Id,
                                        Content = mess.Content,
                                        UserName = mess.RoomUser.User.Username,
                                        // TimeCreated = mess.TimeCreated.ToShortTimeString() + " - " + mess.TimeCreated.ToShortDateString(),
                                        TimeCreated = mess.TimeCreated.ToString(provider: CultureInfo.CreateSpecificCulture("en-UK")),})
                                .OrderByDescending(m => m.Id) // RM 3 way 2 sort list
                                .ToList();
            // messModel.Sort((a,b) => b.Id - a.Id); 
            // messModel.Sort((a,b) => b.Id.CompareTo(a.Id));
            return Ok(messModel);
        }

        [HttpPost("{roomId}/message")]
        public async Task<IActionResult> CreateMessage(int roomId, NewMessageModel model) {
            if(model.Content == "") return Conflict();
            var mess = await roomServices.CreatedMessageAsync(model.UserId, roomId, model.Content);
            if(mess == null) return Conflict();
            return Ok(mess);
        }
    }

    public class NewUserModel {
        public int UserId { get; set; }
    }

    public class MessageModel {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public string TimeCreated { get; set; }
    }

    public class NewMessageModel {
        public string Content { get; set; }
        public int UserId { get; set; }
    }

    //RM: ctrl shift o: show all detail (class, prop, method ...) in file
    //RM: ctrl k + ctrl i: show document

    public class UpdateRoomModel {
        public string Name { get; set; }
    }


    public class RoomUserModel {
        public int RoomId { get; set; }
        public int UserId { get; set; }
    }

    public class NewRoomModel {
        public string RoomName { get; set; }
        public int UserId { get; set; }
    }
}
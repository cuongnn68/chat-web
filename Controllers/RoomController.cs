using System;
using System.Threading.Tasks;
using DiscordRipoff.Data;
using DiscordRipoff.Entities;
using DiscordRipoff.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiscordRipoff.Controllers {
    [ApiController]
    [Route("api/room")]
    public class RoomController : Controller {
        private RoomServices roomServices;
        private AppDbContext dbContext;

        public RoomController (
            RoomServices roomServices,
            AppDbContext dbContext
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

        [HttpPost("{roomId}/user/{userId}")]
        // public async Task<IActionResult> AddUser(int roomId, int userId) {
            //TODO //FIXME model binding not working
        public async Task<IActionResult> AddUser([FromRoute] RoomUserModel model) {
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
            return Ok(mess);
        }

        [HttpPost("{roomId}/message")]
        public async Task<IActionResult> CreateMessage(int roomId, MessageModel model) {
            var mess = await roomServices.CreatedMessageAsync(model.RoomUserId, model.Content);
            if(mess == null) return Conflict();
            // TODO: ues SignalR here
            return Ok(mess);
        }
    }

    public class MessageModel {
        public string Content { get; set; }
        public int RoomUserId { get; set; }
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
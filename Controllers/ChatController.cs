using System;
using System.Globalization;
using System.Threading.Tasks;
using DiscordRipoff.Data;
using DiscordRipoff.Hubs;
using DiscordRipoff.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace DiscordRipoff.Controllers {
    [ApiController]
    [Route("api/chat")]
    public class ChatController : Controller {
        private IHubContext<ChatHub> chat;
        private RoomServices roomServices;
        private AppDbContext dbContext;

        public ChatController(
            IHubContext<ChatHub> chat,
            RoomServices roomServices,
            AppDbContext dbContext
        ) {
            this.chat = chat;
            this.roomServices = roomServices;
            this.dbContext = dbContext;
        }

        [HttpPost("room/{roomId}/{connnectionId}")]
        public async Task<IActionResult> JoinRoom(string connnectionId, int roomId) {
            // TODO check if connectionID exist
            var room = await dbContext.Rooms.FirstOrDefaultAsync(room => room.Id == roomId);
            if(room == null) return BadRequest();
            await chat.Groups.AddToGroupAsync(connnectionId, "room" + room.Id);
            return Ok();
        }

        [HttpDelete("room/{roomId}/{connnectionId}")]
        public async Task<IActionResult> LeaveRoom(string connnectionId, int roomId) {
            // TODO check if connectionID exist
            var room = await dbContext.Rooms.FirstOrDefaultAsync(room => room.Id == roomId);
            if(room == null) return BadRequest();
            await chat.Groups.RemoveFromGroupAsync(connnectionId, "room" + room.Id);
            return Ok();
        }

        [HttpPost("room/{roomId}/message")]
        public async Task<IActionResult> SendMessage(int roomId, NewMessageModel model) { // message have weird order
            Console.WriteLine("new message");
            Console.WriteLine(model);
            // await chat.Clients.Group(roomName).SendAsync();
            if(model.Content == "") return Conflict();
            var mess = await roomServices.CreatedMessageAsync(model.UserId, roomId, model.Content);
            if(mess == null) return Conflict();
            var messModel = new MessageModel {
                Content = mess.Content,
                Id = mess.Id,
                TimeCreated = mess.TimeCreated.ToString(provider: CultureInfo.CreateSpecificCulture("en-UK")),
                UserName = mess.RoomUser.User.Username
            };
            await chat.Clients.Group("room" + roomId).SendAsync("reciveMessage", messModel);
            return Ok(mess);
        }
        
    }
}
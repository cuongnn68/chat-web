using System.Threading.Tasks;
using DiscordRipoff.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DiscordRipoff.Controllers {
    [ApiController]
    [Route("api/rt-chat")]
    public class ChatController : Controller {
        private IHubContext<ChatHub> chat;

        public ChatController(
            IHubContext<ChatHub> chat
        ) {
            this.chat = chat;
        }

        [HttpPost("room/{roomName}/{connnectionId}")]
        public async Task<IActionResult> JoinRoom(string connnectionId, string roomName) {
            await chat.Groups.AddToGroupAsync(connnectionId, roomName);
            return Ok();
        }

        [HttpDelete("room/{roomName}/{connnectionId}")]
        public async Task<IActionResult> LeaveRoom(string connnectionId, string roomName) {
            await chat.Groups.RemoveFromGroupAsync(connnectionId, roomName);
            return Ok();
        }

        [HttpPost("room/{roomName}")]
        public async Task<IActionResult> SendMessage(string message, string roomName) {
            // await chat.Clients.Group(roomName).SendAsync();
            return Ok();
        }
        
    }
}
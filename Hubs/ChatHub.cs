using System;
using System.Threading.Tasks;
using DiscordRipoff.Controllers;
using DiscordRipoff.Utils;
using Microsoft.AspNetCore.SignalR;

namespace DiscordRipoff.Hubs {
    public class ChatHub : Hub {
        [JWTAuthentication]
        public string GetConnectionId() => Context.ConnectionId;

        public void JoinTestRoom() {
            this.Groups.AddToGroupAsync(Context.ConnectionId, "testRoom");
        }
        public void MessTestRoom(string mess) {
            Clients.Group("testRoom").SendAsync("reciveTest", mess);
        }

        public void ShowInfo() {
            // this.Clients.All
            // Context.
            int? a = null;
            Console.WriteLine(a);
            Console.WriteLine("run here");
            this.Clients.All.SendAsync("AddNewMessage", "ahahaha");
            this.Groups.AddToGroupAsync("test", Context.ConnectionId);
        }

        public void TestJson() {
            var model = new MessageModel {
                Content = " tes content mess",
                Id = 686688,
                UserName = "me"
            };
            Clients.All.SendAsync("testJson", model);
        }


        // public override bool Equals(object obj) {
        //     return base.Equals(obj);
        // }

        // public override int GetHashCode() {
        //     return base.GetHashCode();
        // }

        // public override Task OnConnectedAsync() {
        //     return base.OnConnectedAsync();
        // }

        // ? also remove in group???
        // TODO: if not use this to remove all user group when sign out
        public override Task OnDisconnectedAsync(Exception exception) { 
            Console.WriteLine(Context.ConnectionId + " disconnected");
            return base.OnDisconnectedAsync(exception);
        }

        // public override string ToString() {
        //     return base.ToString();
        // }

        // protected override void Dispose(bool disposing) {
        //     base.Dispose(disposing);
        // }
    }
}
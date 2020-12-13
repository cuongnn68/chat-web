using System;
using System.Threading.Tasks;
using DiscordRipoff.Utils;
using Microsoft.AspNetCore.SignalR;

namespace DiscordRipoff.Hubs {
    public class ChatHub : Hub {
        [JWTAuthentication]
        public string GetConnectionId() => Context.ConnectionId;

        

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override Task OnConnectedAsync() {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception) {
            return base.OnDisconnectedAsync(exception);
        }

        public override string ToString() {
            return base.ToString();
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using DiscordRipoff.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscordRipoff.Services {
    public class UserServices {
        private DbContext dbContext;

        public UserServices(DbContext dbContext) {
            this.dbContext = dbContext;
        }
        public async Task<bool> CreateUserAsync(User user) {
            var exits = await dbContext.FindAsync<User>(user.Id) != null;
            if(exits) return false;
            await dbContext.AddAsync<User>(user);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ValidateUserAsync (string username, string password) {
            var user = await dbContext.Set<User>()
                .FirstOrDefaultAsync(user => user.Username == username);
            if(user != null && user.Password == password) return true;
            return false;
        }
    }
}
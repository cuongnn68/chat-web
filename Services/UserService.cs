using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscordRipoff.Data;
using DiscordRipoff.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscordRipoff.Services {
    public class UserServices {
        private AppDbContext dbContext;

        public UserServices(AppDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public async Task<bool> CreateUserAsync(User user) {
            var exits = await dbContext.FindAsync<User>(user.Id) != null;
            if(exits) return false;
            await dbContext.AddAsync<User>(user);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<User> UpdateAsync(int userId, string fullName = null, string email = null, string phone = null) {
            var user = await dbContext.Users.FindAsync(userId);
            if(user == null) return null;
            // DONE make value == null and only update value != null
            if(fullName != null && fullName != "") user.FullName = fullName;
            if(email != null && email != "") user.Email = email;
            if(phone != null && phone != "") user.Phone = phone;
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        // TODO finish this change
        public async Task<bool> UpdatePasswordAsync(int userId, string password) {
            // TODO check password
            var user = await dbContext.Users.FindAsync(userId);
            if(user == null) return false;
            // TODO logout all exist JWT
            
            user.Password = password;
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> SearchAsync(int id, string name) {
            return await dbContext.Users.Where(user => user.Id.ToString().Contains(id.ToString()) 
                                            || user.Username.Contains(name))
                                .ToListAsync();
        }

        public async Task<bool> ValidateUserAsync (string username, string password) {
            var user = await dbContext.Set<User>()
                .FirstOrDefaultAsync(user => user.Username == username);
            if(user != null && user.Password == password) return true;
            return false;
        }

        public async Task<List<Room>> GetRoomAsync(int userId) {
            return await dbContext.RoomUsers.Include(ru => ru.Room)
                            .Where(ru => ru.UserId == userId)
                            .Select(ru => ru.Room)
                            .ToListAsync();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscordRipoff.Data;
using DiscordRipoff.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscordRipoff {
    public class SearchService {
        private AppDbContext dbContext;

        public SearchService(AppDbContext dbContext) {
            this.dbContext = dbContext;

        }
        public async Task<List<User>> SearchAllUserAsync(string keyword) {
            if(keyword == null || keyword == string.Empty) return new List<User>();
            var isNumber = Int32.TryParse(keyword, out var numKeyword);
            // Func<User,bool> condition = user => (user.Id == numKeyword) 
            //                                 || (user.Username.Contains(keyword)
            //                                 || (user.FullName.Contains(keyword)));
            // RM cant use asyn with delegate defined outside
            return await dbContext.Users
                                .Where(u => (isNumber && (u.Id == numKeyword))
                                        || (u.Username.Contains(keyword))
                                        || (u.FullName.Contains(keyword)))
                                .ToListAsync();

        }

        public async Task<List<Room>> SearchAllRoomAsync(string keyword) {
            if(keyword == null || keyword == string.Empty) return new List<Room>();
            var isNumber = Int32.TryParse(keyword, out var numKeyword);
            return await dbContext.Rooms
                                .Where(r => (isNumber && (r.Id == numKeyword)) || (r.Name.Contains(keyword)))
                                .ToListAsync();
            
        }
    }
}
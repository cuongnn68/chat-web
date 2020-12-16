using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscordRipoff.Data;
using DiscordRipoff.Entities;
using DiscordRipoff.Utils;
using Microsoft.EntityFrameworkCore;

namespace DiscordRipoff.Services {
    public class RoomServices {
        private AppDbContext dbContext;

        public RoomServices (
            AppDbContext dbContext
        ) {
            this.dbContext = dbContext;
        }

        public async Task<Room> CreateAsync(string roomName, int userId) {
            var room = new Room {
                Name = roomName,
                DayCreated = DateTime.Now
            };
            // await dbContext.AddAsync(room);
            // await dbContext.SaveChangesAsync();
            var roomUser = new RoomUser {
                // RoomId = room.Id,
                Room = room,
                UserId = userId,
                TimeJoin = DateTime.Now,
                Role = RoomRole.ADMIN
            };
            await dbContext.AddAsync(roomUser);
            await dbContext.SaveChangesAsync();
            return room;
        }
        public async Task<bool> RemoveAsync(int id) {
            Console.WriteLine(id);
            var room = await dbContext.Rooms.Include(room => room.RoomUsers).FirstOrDefaultAsync(room => room.Id == id);
            Console.WriteLine(room == null);
            // dbContext.Remove(room.RoomMessages);
            // dbContext.Remove(room.RoomUsers);
            dbContext.Remove(room);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, string name) {
            var room = new Room {
                Id = id,
                Name = name,
            };
            dbContext.Update(room);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> GetAllUsersAsync(int roomId) {
            return await dbContext.RoomUsers
                                .Include(ru => ru.User)
                                .Where(ru => ru.RoomId == roomId)
                                .Select(ru => ru.User)
                                .ToListAsync();
        }

        public async Task<RoomUser> AddUserAsync(int roomId, int userId) {
            var roomUser = new RoomUser {
                RoomId = roomId,
                UserId = userId,
            };
            await dbContext.AddAsync(roomUser);
            await dbContext.SaveChangesAsync();
            return roomUser;
        }

        
        public async Task<bool> RemoveUserAsync (int userId, int roomId) {
            
            var roomUser = await dbContext.RoomUsers
                                        .FirstOrDefaultAsync(rUser => 
                                            rUser.UserId == userId && rUser.RoomId == roomId);
            if(roomUser == null) return false;
            dbContext.Remove(roomUser);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Room>> SearchAsync (int id, string name) {
            return await dbContext.Rooms.Where(room => 
                                            room.Id.ToString().Contains(id.ToString())
                                            || room.Name.Contains(name))
                                .ToListAsync();
        }

        public async Task<List<RoomMessage>> GetMessagesAsync(int roomId) {
            return await dbContext.RoomMessages
                                .Include(rm => rm.RoomUser)
                                .ThenInclude(ru => ru.User)
                                .Where(rm => rm.RoomUser.RoomId == roomId)
                                .ToListAsync();
        }

        public async Task<RoomMessage> CreatedMessageAsync(int userId, int roomId, string content) {
            var roomUser = await dbContext.RoomUsers.FirstOrDefaultAsync(ru => ru.UserId == userId && ru.RoomId == roomId);
            if(roomUser == null) return null;
            var mess = new RoomMessage {
                Content = content,
                RoomUserId = roomUser.Id,
            };
            dbContext.Add(mess);
            // ? seem terrible
            await dbContext.SaveChangesAsync();
            mess = await dbContext.RoomMessages
                                .Where(m => m.Id == mess.Id)
                                .Include(rm => rm.RoomUser)
                                .ThenInclude(ru => ru.User)
                                .FirstAsync();
            return mess;
        }
    }
}
using DiscordRipoff.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscordRipoff.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomUser> RoomUsers { get; set; }
        
        public DbSet<JWTBlacklist> Blacklist { get; set; }

    }
}
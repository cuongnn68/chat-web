using System;
using DiscordRipoff.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscordRipoff.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoomUser>()
                        .HasAlternateKey(roomUser => new {roomUser.RoomId, roomUser.UserId});
            modelBuilder.Entity<User>()
                        .HasIndex(user => user.Username) // ? vs Property
                        .IsUnique();
                        
                        // ? vs
                        
                        // .Entity<User>(user => {
                        //     user.HasIndex(u => u.Id)
                        //         .IsUnique();
                        // });


                        
                        // ?: vs
                        // .Property(user => user.Username)
                        // .HasCheckConstraint("UNIQUE", "UNIQUE");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomUser> RoomUsers { get; set; }
        public DbSet<RoomMessage> RoomMessages { get; set; }
        public DbSet<JWTBlacklist> Blacklist { get; set; }

    }
}
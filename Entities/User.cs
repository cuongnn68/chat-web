using System.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordRipoff.Entities { //TODO: migrations again after update entities
    public class User {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // auto increase
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // TODO: hash password
        public string FullName { get; set; }
        public string Email { get; set; }
        
    }

    public class UserStoreTest {
        public static List<User> GetUser() {
            return new List<User> {
                new User {Id = 1, Username ="ok"},
                new User {Id = 2, Username ="cuong"},
                new User {Id = 3, Username ="nana"},
                new User {Id = 4, Username ="haha"},
                new User {Id = 5, Username ="hoho"},
                new User {Id = 6, Username ="coco"},
                new User {Id = 7, Username ="momo"},
            };
        }
    }
}
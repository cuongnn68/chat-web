using System.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordRipoff.Entities {
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
                new User {Id = 3, Username ="nana"}
            };
        }
    }
}
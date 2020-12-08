using System.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System;

namespace DiscordRipoff.Entities {
    //TODO: migrations again after update entities
    public class User {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // auto increase
        public int Id { get; set; }
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage="test regex")]
        [Required]
        public string Username { get; set; }

        [JsonIgnore]
        [Required]
        public string Password { get; set; }
        // TODO: hash password

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime? DateOfBirth { get; set; }

        

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
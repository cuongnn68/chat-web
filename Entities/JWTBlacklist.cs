using System;
using System.ComponentModel.DataAnnotations;

namespace DiscordRipoff.Entities {
    public class JWTBlacklist {
        [Key]
        public int Id { get; set; }
        public string JWT { get; set; }
        public DateTime TimeExpired { get; set; } // todo: change type in database
    }
}
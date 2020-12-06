using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DiscordRipoff.Utils;

namespace DiscordRipoff.Entities {
    public class RoomUser {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public RoomRole MyProperty { get; set; }

        public DateTime TimeJoin { get; set; }

        public int UserId { get; set; }

        public int RoomId { get; set; }

        public User User { get; set; }
        public Room Room { get; set; }
        public ICollection<RoomMessage> RoomMessages { get; set; }

    }

}
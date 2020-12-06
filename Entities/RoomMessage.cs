using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiscordRipoff.Entities {
    public class RoomMessage {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public int RoomUserId { get; set; }


        public RoomUser RoomUser { get; set; }
    }
}
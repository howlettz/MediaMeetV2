using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MediaMeetV2.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int recipiantID { get; set; }
        [Required]
        public String messageText { get; set; }
        [Required]
        public DateTime dateSent { get; set; }
        [Required]
        public Boolean read { get; set; }
        [Required]
        public int threadID { get; set; }
    }
}
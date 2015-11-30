using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MediaMeetV2.Models
{
    public class Friend
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MemberID { get; set; }
        [Required]
        public DateTime dateFriended { get; set; }

    }
}
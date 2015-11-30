using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MediaMeetV2.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("User Name (Nickname)")]
        public String userName { get; set; }   //Identity
        [Required]
        [DisplayName("Member Name (Real Name)")]
        public String memberName { get; set; } //Displayed on site
        [Required]
        public DateTime dateJoined { get; set; }
        [Required]
        public DateTime lastLogin { get; set; }
        public Profile assocProfile { get; set; }
        public int ProfileID { get; set; }


    }
}
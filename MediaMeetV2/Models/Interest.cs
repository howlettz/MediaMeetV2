using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MediaMeetV2.Models
{
    public class Interest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String name { get; set; }
        public String description { get; set; }

        
        public List<Profile> assocProfiles { get; set; }
    }
}
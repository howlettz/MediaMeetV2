using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MediaMeetV2.Models
{
    public class Demographics
    {
        [Key]
        public int Id { get; set; }

        public String city { get; set; }
        public String state { get; set; }
        public String country { get; set; }
        [Required]
        public DateTime birthDate { get; set; }
        [Required]
        public String gender { get; set; }

    }
}
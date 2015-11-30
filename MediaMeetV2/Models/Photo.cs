using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MediaMeetV2.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String filePath { get; set; }
        [Required]
        public String description { get; set; }
        [Required]
        public DateTime dateAdded { get; set; }
        [Required]
        public Boolean profilePicture { get; set; }

    }
}
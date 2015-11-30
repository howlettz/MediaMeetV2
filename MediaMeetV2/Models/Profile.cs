using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MediaMeetV2.Models
{
    public class Profile
    {
        [Key]
        public int Id { get; set; }
        public String introduction { get; set; }
        public Member assocMember { get; set; }

        public Demographics assocDemographics { get; set; }
        public int DemographicsID { get; set; }

        public List<Interest> assocInterests { get; set; }

        public List<Photo> assocPhotos { get; set; }

        public List<Friend> assocFriends { get; set; }

        public List<Message> assocMessages { get; set; }

    }
}
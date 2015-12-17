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

        public virtual IList<Interest> Interests { get; set; }

        public virtual IList<Photo> assocPhotos { get; set; }

        public virtual IList<Friend> Friends { get; set; }

        public virtual IList<Message> assocMessages { get; set; }

    }
}
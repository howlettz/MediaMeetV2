using System;
using System.Collections.Generic;

namespace MediaMeetV2.Models
{
    public class UserViewModel
    {
        //Identifiers
        public int Id { get; set; }
        public string userName { get; set; }
        public string memberName { get; set; }

        //Personal
        public string introduction { get; set; }
        public DateTime birthDate { get; set; }
        public string gender { get; set; }
        public IList<Interest> Interests { get; set; }
        public IList<Friend> Friends { get; set; }

        //Location
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }

        //Meta
        public DateTime dateJoined { get; set; }
        public DateTime lastLogin { get; set; }
    }
}

using MediaMeetV2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MediaMeetV2.Models
{
    public class MediaMeetV2DbContext : DbContext
    {

        public MediaMeetV2DbContext() : base("name = MediaMeetV2DbContext")
        {
        }

        public System.Data.Entity.DbSet<MediaMeetV2.Models.Member> Member { get; set; }

        public System.Data.Entity.DbSet<MediaMeetV2.Models.Profile> Profile { get; set; }

        public System.Data.Entity.DbSet<MediaMeetV2.Models.Demographics> Demographics { get; set; }

        public System.Data.Entity.DbSet<MediaMeetV2.Models.Interest> Interest { get; set; }

        public System.Data.Entity.DbSet<MediaMeetV2.Models.Friend> Friend { get; set; }

        public System.Data.Entity.DbSet<MediaMeetV2.Models.Photo> Photo { get; set; }

        public System.Data.Entity.DbSet<MediaMeetV2.Models.Message> Message { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Member>()
            .HasOptional<Profile>(m => m.assocProfile)
            .WithRequired(m => m.assocMember)
            .Map(p => p.MapKey("MemberId"));
        }

    }
}
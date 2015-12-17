using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MediaMeetV2.Models;
using System.Data.Entity;
using System.Web.SessionState;
using System.Web.UI;

namespace MediaMeetV2.Models
{

    public class UserRepository : Page
    {
        private MediaMeetV2DbContext db = new MediaMeetV2DbContext();


        public UserRepository()
        {

            Mapper.CreateMap<Member, UserViewModel>()

                .ForMember(x => x.userName, opt => opt.MapFrom(src => src.userName))
                .ForMember(x => x.memberName, opt => opt.MapFrom(src => src.memberName))
                .ForMember(x => x.introduction, opt => opt.MapFrom(src => src.assocProfile.introduction))
                .ForMember(x => x.birthDate, opt => opt.MapFrom(src => src.assocProfile.assocDemographics.birthDate))
                .ForMember(x => x.gender, opt => opt.MapFrom(src => src.assocProfile.assocDemographics.gender))
                .ForMember(x => x.city, opt => opt.MapFrom(src => src.assocProfile.assocDemographics.city))
                .ForMember(x => x.state, opt => opt.MapFrom(src => src.assocProfile.assocDemographics.state))
                .ForMember(x => x.country, opt => opt.MapFrom(src => src.assocProfile.assocDemographics.country))
                .ForMember(x => x.dateJoined, opt => opt.MapFrom(src => src.dateJoined))
                .ForMember(x => x.dateJoined, opt => opt.MapFrom(src => src.lastLogin))
                .ForMember(x => x.Interests, opt => opt.MapFrom(src => src.assocProfile.Interests))
                .ForMember(x => x.Friends, opt => opt.MapFrom(src => src.assocProfile.Friends));

            Mapper.AssertConfigurationIsValid();
        }

        public MediaMeetV2DbContext getDB()
        {
            return db;
        }


        public UserViewModel UserInfo(int id)
        {

            Member user = (from mem in db.Member where mem.Id == id select mem).Single();
            Profile prof = (from pro in db.Profile where pro.Id == user.ProfileID select pro).Single();
            Demographics demo = (from dem in db.Demographics where dem.Id == prof.DemographicsID select dem).Single();
            var userv = Mapper.Map<UserViewModel>(user);

            userv.introduction = prof.introduction;
            userv.birthDate = demo.birthDate;
            userv.gender = demo.gender;
            userv.city = demo.city;
            userv.state = demo.state;
            userv.country = demo.country;
            userv.Id = user.Id;

            if (prof.Interests == null)
            {
                prof.Interests = new List<Interest>();
            }

            if (prof.Friends == null)
            {
                prof.Friends = new List<Friend>();
            }
            db.SaveChanges();

            userv.Interests = prof.Interests;
            userv.Friends = prof.Friends;

            return userv;
        }

        public UserViewModel createUser(Member mem, Profile pro, Demographics dem)
        {
            Member newMem = mem;
            Profile newProf = pro;
            Demographics newDemo = dem;

            newMem.dateJoined = DateTime.Now;
            newMem.lastLogin = DateTime.Now;

            db.Demographics.Add(newDemo);
            db.SaveChanges();

            
            db.Member.Add(newMem);
            db.SaveChanges();
            
            newProf.assocDemographics = newDemo;
            newProf.DemographicsID = newDemo.Id;
            newProf.assocMember = newMem;
            db.Profile.Add(newProf);
            db.SaveChanges();
            
            db.SaveChanges();

            newMem.assocProfile = newProf;
            newMem.ProfileID = newProf.Id;
            db.SaveChanges();

            var userv = Mapper.Map<UserViewModel>(newMem);

            userv.introduction = newProf.introduction;
            userv.birthDate = newDemo.birthDate;
            userv.gender = newDemo.gender;
            userv.city = newDemo.city;
            userv.state = newDemo.state;
            userv.country = newDemo.country;
            userv.Id = newMem.Id;

            return userv;
        }

        public List<UserViewModel> UserViewList()
        {
            var memList = db.Member.ToList();
            List<UserViewModel> userList = new List<UserViewModel>();

            for(int i = 0; i < memList.Count; i++)
            {
                Member memb = memList.ElementAt(i);
                Profile prof = (from pro in db.Profile where pro.Id == memb.ProfileID select pro).Single();
                Demographics demo = (from dem in db.Demographics where dem.Id == prof.DemographicsID select dem).Single();
                var userv = Mapper.Map<UserViewModel>(memb);

                userv.introduction = prof.introduction;
                userv.birthDate = demo.birthDate;
                userv.gender = demo.gender;
                userv.city = demo.city;
                userv.state = demo.state;
                userv.country = demo.country;
                userv.Id = memb.Id;

                userList.Add(userv);
            }

            return userList;
        } 

        public void UserAddInterest(Interest inter, int id)
        {
            var interes = db.Interest.Add(inter);
            db.SaveChanges();

            db.Member.Include("profile");
            db.Profile.Include("Interests");

            Member memb = db.Member.Find(id);
            Profile prof = db.Profile.Find(memb.ProfileID);

            if (prof.Interests == null)
            {
                prof.Interests = new List<Interest>();

                db.SaveChanges();
            }

            if (interes.assocProfiles == null)
            {
                interes.assocProfiles = new List<Profile>();

                db.SaveChanges();
            }

            prof.Interests.Add(interes);
            interes.assocProfiles.Add(prof);

            db.Entry(prof).State = EntityState.Modified;
            db.Entry(interes).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void UserAddFriend(Friend frie, int id)
        {
            Member memb = db.Member.Find(id);
            Profile prof = db.Profile.Find(memb.ProfileID);

            frie.dateFriended = DateTime.Now;

            frie.assocProfile = prof;

            var frien = db.Friend.Add(frie);
            db.SaveChanges();

            db.Member.Include("profile");
            db.Profile.Include("Friends");

            

            if (prof.Friends == null)
            {
                prof.Friends = new List<Friend>();

                db.SaveChanges();
            }

            prof.Friends.Add(frien);
            frien.assocProfile = prof;

            db.Entry(prof).State = EntityState.Modified;
            db.Entry(frien).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

    }
}
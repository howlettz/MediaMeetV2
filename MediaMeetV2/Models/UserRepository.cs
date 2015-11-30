using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MediaMeetV2.Models;


namespace MediaMeetV2.Models
{

    public class UserRepository
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
                .ForMember(x => x.assocInterests, opt => opt.MapFrom(src => src.assocProfile.assocInterests));

            Mapper.AssertConfigurationIsValid();
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
            userv.assocInterests = prof.assocInterests;

            if (userv.assocInterests == null)
            {
                userv.assocInterests = new List<Interest>();
            }

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

            Member memb = (from mem in db.Member where mem.Id == id select mem).Single();
            Profile prof = (from pro in db.Profile where pro.Id == memb.ProfileID select pro).Single();

            if (prof.assocInterests == null)
            {
                prof.assocInterests = new List<Interest>();
                db.SaveChanges();
            }

            prof.assocInterests.Add(interes);
            db.SaveChanges();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

    }
}
using MediaMeetV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediaMeetV2.Controllers
{
    public class UserViewController : Controller
    {
        private UserRepository repo = new UserRepository();
        UserViewModel newUser;
        // GET: UserView
        public ActionResult ViewUser(int id)
        {
            return View(repo.UserInfo(id));
        }

        public ActionResult UserCreate ([Bind(Include = "Id, userName, memberName, dateJoined, lastLogin, assocProfile, ProfileID")] Member mem,
                        [Bind(Include = "Id, introduction, assocDemographics, DemographicsID, assocInterests, assocInterests, assocFriends, assocMessages")] Profile pro,
                        [Bind(Include = "Id, city, state, country, birthDate, gender")] Demographics dem)
        {
            if (ModelState.IsValid)
            {
              newUser =  repo.createUser(mem, pro, dem);
            }
            
            return View();
        }

        public ActionResult UserViewList()
        {
            return View(repo.UserViewList());
        }

        public ActionResult UserAddInterest ([Bind(Include = "Id, name, description")] Interest inter, int id)
        {
            if (ModelState.IsValid)
            {
                repo.UserAddInterest(inter, id);
            }

            return View();
        }
            
    }
}
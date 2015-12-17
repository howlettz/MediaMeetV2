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
        private MediaMeetV2DbContext db = new MediaMeetV2DbContext();
        UserViewModel newUser;
        // GET: UserView
        public ActionResult ViewUser(int id)
        {
            ViewBag.Friendz = new List<String>();
            String person;
            foreach (var Frie in repo.UserInfo(id).Friends)
            {
                try {
                   person  = (from x in db.Friend where x.assocProfile.assocMember.Id == Frie.MemberID && x.MemberID == id select x.assocProfile.assocMember.userName).Single();
                }
                catch (System.InvalidOperationException)
                {
                    person = null;
                }
                if (person != null)
                    ViewBag.Friendz.Add(person);
                //ViewBag.Friendz.Add((from n in db.Member where n.Id == Frie.MemberID select n.userName).Single());
            }

            return View(repo.UserInfo(id));
        }

        public ActionResult UserCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserCreate ([Bind(Include = "Id, userName, memberName, dateJoined, lastLogin, assocProfile, ProfileID")] Member mem,
                        [Bind(Include = "Id, introduction, assocDemographics, DemographicsID, assocInterests, assocInterests, assocFriends, assocMessages")] Profile pro,
                        [Bind(Include = "Id, city, state, country, birthDate, gender")] Demographics dem)
        {
            if (ModelState.IsValid)
            {
              newUser =  repo.createUser(mem, pro, dem);
                db.SaveChanges();
                return RedirectToAction("UserViewList");
            }
            
            return View();
        }

        public ActionResult UserViewList()
        {
            return View(repo.UserViewList());
        }
        public ActionResult UserAddInterest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserAddInterest ([Bind(Include = "Id, name, description")] Interest inter, int id)
        {
            if (ModelState.IsValid)
            {
                repo.UserAddInterest(inter, id);
                return RedirectToAction("ViewUser", new {id = id });
            }

            return View();
        }

        public ActionResult UserAddFriend()
        {
            ViewBag.MemberID = new SelectList(db.Member, "Id", "userName");
            return View();
        }

        [HttpPost]
        public ActionResult UserAddFriend([Bind(Include = "Id, MemberID, dateFriended, assocProfiles")] Friend frien, int id)
        {
            ViewBag.MemberID = new SelectList(db.Member, "Id", "userName");

            if (ModelState.IsValid)
            {
                repo.UserAddFriend(frien, id);
                return RedirectToAction("ViewUser", new { id = id });
            }

            return View();
        }

    }
}
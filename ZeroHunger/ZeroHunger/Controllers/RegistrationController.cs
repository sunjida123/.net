using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.EF;

namespace ZeroHunger.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User u)
        {
            var db = new NGOV2Entities();
            db.Users.Add(u);
            db.SaveChanges();
            return RedirectToAction("Login");

        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User u)
        {
            var db = new NGOV2Entities();
            var data = (from us in db.Users
                        where us.Username == u.Username && us.Password == u.Password
                        select us).SingleOrDefault();
            if (data != null)
            {
                Session["userid"] = data.ID;
                Session["username"] = data.Username;
                Session["userpassword"] = data.Password;
                Session["username"] = data.Name;
                Session["userrole"] = data.Role;
                Session["useremail"] = data.Email;
                Session["userphone"] = data.ContactNumber;

                if (Session["userrole"] != null && Session["userrole"].ToString() == "admin") { return RedirectToAction("ShowAllRequest", "NGO"); }
                else if (Session["userrole"] != null && Session["userrole"].ToString() == "employee") { return RedirectToAction("Dashboard", "Employee"); }

            }
            return RedirectToAction("SignUp", "Registration");
        }


        [HttpGet]
        public ActionResult LoginRestaurant()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginRestaurant(Restaurant r)
        {
            var db = new NGOV2Entities();
            var data = (from rn in db.Restaurants
                        where rn.RestaurantID == r.RestaurantID && rn.Password == r.Password
                        select rn).SingleOrDefault();
            if (data != null)
            {
                Session["restaurantid"] = data.RestaurantID;
                Session["restaurantname"] = data.RestaurantName;
                Session["Location"] = data.Location;
                Session["contactperson"] = data.ContactPerson;
                Session["contactnumber"] = data.ContactNumber;
                Session["email"] = data.EmailAddress;
                

                return RedirectToAction("Dashboard", "Restaurant"); 
               

            }
            return RedirectToAction("SignUp", "Registration");
        }
    }
}
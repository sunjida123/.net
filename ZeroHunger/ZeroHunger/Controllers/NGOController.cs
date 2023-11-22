using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.EF;

namespace ZeroHunger.Controllers
{
    public class NGOController : Controller
    {
        // GET: NGO
        public ActionResult ShowAllRequest()
        {
            var db = new NGOV2Entities();
            ViewBag.Employees = (from us in db.Users
                                 where us.Role == "employee"
                                 select us).ToList();
            var data = (from cr in db.CollectRequests
                        where cr.Status == "Open"
                        select cr).ToList();
            return View(data);
        }



        public ActionResult ShowAllEmployees()
        {
            var db = new NGOV2Entities();
            var data = (from us in db.Users
                        where us.Role == "employee"
                        select us).ToList();
            return View(data);
        }


        public ActionResult ShowAllAssignments()
        {
            var db = new NGOV2Entities();
            var data = (from a in db.Assignments
                        where a.AssignmentStatus == "Assigned" 
                        select a).ToList();

            return View(data);
        }

        [HttpGet]
        public ActionResult AssignEmployee(int id)
        {
            var db = new NGOV2Entities();
            ViewBag.Employees = (from us in db.Users
                                 where us.Role == "employee" && us.Status =="Available"
                                 select us).ToList();
            var data = db.CollectRequests.Find(id);
            return View(data);
        }
        [HttpPost]
        public ActionResult AssignEmployee(Assignment a)
        {
            var db = new NGOV2Entities();

            db.Assignments.Add(a);
            db.SaveChanges();
            var data = db.CollectRequests.Find(a.CollectRequestID);
            data.Status = "Accepted";
            db.SaveChanges();
            var emp = db.Users.Find(a.EmployeeID);
            emp.Status = "Assigned";
            db.SaveChanges();
            return RedirectToAction("ShowAllAssignments");
        }


        
        public ActionResult DistibutionRecord()
        {
            var db = new NGOV2Entities();
            var data = db.DistributionRecords.ToList();

            return View(data);
        }
        
   



        public ActionResult ShowAllRestaurants()
        {
            var db = new NGOV2Entities();
            var data = db.Restaurants.ToList();

            return View(data);
        }

        [HttpGet]
        public ActionResult AddRestaurants()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddRestaurants(Restaurant r)
        {
            var db = new NGOV2Entities();
            db.Restaurants.Add(r);
            db.SaveChanges();
            return RedirectToAction("ShowAllRestaurants");

        }
    }
}
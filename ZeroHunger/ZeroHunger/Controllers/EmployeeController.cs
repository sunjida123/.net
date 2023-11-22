using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.EF;

namespace ZeroHunger.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Assignments()
        {
            var id = Convert.ToInt32(Session["userid"]);
            var db = new NGOV2Entities();
            var data = (from a in db.Assignments
                        where a.EmployeeID == id && a.AssignmentStatus == "Assigned"
                        select a);
            return View(data);
        }

        public ActionResult Distributed(int id)
        {
            var employeeid = Convert.ToInt32(Session["userid"]);
            var db = new NGOV2Entities();

            var data = db.Assignments.Find(id);
            var collectrequestid = data.CollectRequestID;
            DistributionRecord record = new DistributionRecord
            {
                CollectRequestID = collectrequestid,
                EmployeeID = employeeid,
                DistributionTime = DateTime.Now,
       
                Status = "Completed"

            };
            db.DistributionRecords.Add(record);
            db.SaveChanges();
            var asdata = db.Assignments.Find(id);
            asdata.AssignmentStatus = "Completed";
            db.SaveChanges();
            var crdata = db.CollectRequests.Find(collectrequestid);
            crdata.Status = "Completed";
            db.SaveChanges();
            var empdata = db.Users.Find(employeeid);
            empdata.Status = "Available";
            db.SaveChanges();

            return RedirectToAction("Assignments");
        }

        public ActionResult History()
        {
            var id = Convert.ToInt32(Session["userid"]);
            var db = new NGOV2Entities();
            var data = (from dr in db.DistributionRecords
                        where dr.EmployeeID == id 
                        select dr).ToList();
            return View(data);
        }

    }
    
}
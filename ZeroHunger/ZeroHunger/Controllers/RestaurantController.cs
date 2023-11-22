using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.EF;

namespace ZeroHunger.Controllers
{
    public class RestaurantController : Controller
    {
        // GET: Restaurant
        public ActionResult Dashboard()
        {
            return View();
        }
        [HttpGet] 
        public ActionResult CreateCollectRequest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCollectRequest(CollectRequest cr)
        {
            var db = new NGOV2Entities();
            db.CollectRequests.Add(cr);
            db.SaveChanges();
            return RedirectToAction("ShowRequests");
        }

        public ActionResult ShowRequests()
        {
            var id = Convert.ToInt32(Session["restaurantid"]);
            var db = new NGOV2Entities();
            var data = (from cr in db.CollectRequests
                        where cr.RestaurantID == id 
                        select cr).ToList();

            return View(data);
        }
    }
}
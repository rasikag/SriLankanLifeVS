using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SriLankanLifeVS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            List<string> districts = new List<string> {"Ampara","Anuradapura","Badulla","Baticaloa","Colombo","Galle","Gampaha",
                                                        "Hambantota","Jaffna","Kalutara","Kandy","Kegalle","Kilinochchi","Kurunegala",
                                                        "Mannar","Matale","Matara","Monaragala","Mullaitivu","Nuwara Eliya",
                                                        "Polonnaruwa","Puttalam","Ratnapura","Trincomalee","Vavunia"};
            List<string> events = new List<string> {"Wild Safari","Camping","Rafting","Whale Watching","Bird Watching","Waterfalls","Hiking",
                                                        "Diving","Perahera","Cricket Matches","Cultural Festival","National Festivals","Sport Festivals","Religious Festival",
                                                        "Adventures Events","Entertainment Events","Nature Trails","Motor Cross","Educational Festivals","Volunteering",
                                                        "Surfing","Meditation","Ayurvedic Treatments","Conferences" };

            ViewBag.Events = events;
            ViewBag.Districts = districts;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


       
        public ActionResult PlaceInDetailView()
        {
            return View();
        }

        public ActionResult Place()
        {
            return View();
        }


    }
}
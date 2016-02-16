using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SriLankanLifeVS.Models.TravelContext;

namespace SriLankanLifeVS.Controllers
{
    public class HomeController : Controller
    {
        private TravelContext _db = new TravelContext();

        public ActionResult Index()
        {
            var districts = _db.Districts.ToList();

            //List<string> districts = new List<string> {"Ampara","Anuradapura","Badulla","Baticaloa","Colombo","Galle","Gampaha",
            //                                            "Hambantota","Jaffna","Kalutara","Kandy","Kegalle","Kilinochchi","Kurunegala",
            //                                            "Mannar","Matale","Matara","Monaragala","Mullaitivu","Nuwara Eliya",
            //                                            "Polonnaruwa","Puttalam","Ratnapura","Trincomalee","Vavunia"};
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

        public ActionResult Place(int id = 1)
        {
            List<VMTownInView> town = _db.Towns.Where(t => t.DistrictId == id).Select(p => new VMTownInView
            {
                Id = p.Id,
                TownName = p.TownName
            }).ToList();

            ViewBag.Towns = town;
            return View();
        }


        public ActionResult PlacesInTown(int id = 1)
        {
            List<VMPlaceInView> plc = _db.Places.Where(t => t.TownId == id).Select(p => new VMPlaceInView
            {
                Id = p.Id,
                PlaceName = p.PlaceName
            }).ToList();

            ViewBag.Towns = plc;
            return View();
        }


    }


    public class VMTownInView
    {
        public int Id { get; set; }
        public string TownName { get; set; }
    }

    public class VMPlaceInView
    {

        public Guid Id { get; set; }

        public string PlaceName { get; set; }

    }


}
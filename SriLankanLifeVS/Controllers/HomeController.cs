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



        // this will show all towns 

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

        // show all places in a town

        public ActionResult PlacesInTown(int id = 1)
        {
            List<VMPlaceInView> plc = _db.Places.Where(t => t.TownId == id).Select(p => new VMPlaceInView
            {
                Id = p.Id,
                PlaceName = p.PlaceName
            }).ToList();

            List<CovGuid> covguid = new List<CovGuid>();


            for (int i = 0; i<plc.Count; i++)
            {
                CovGuid g = new CovGuid();
                g.Id = plc[i].Id.ToString();
                g.PlaceName = plc[i].PlaceName;

                covguid.Add(g);
            }

            ViewBag.Towns = covguid;
            return View();
        }

        // show a details in a place

        public ActionResult PlaceDetails(string strId)
        {
            Guid id = Guid.Parse(strId);

            if (id!=null)
            {
                var place = _db.Places.Where(p => p.Id == id).Select(t => new VMPlaceInHome
                {
                    Id = t.Id,
                    PlaceName = t.PlaceName,
                    Longitude = t.Longitude,
                    Latitude = t.Latitude,
                    Address = t.Address,
                    Description = t.Description,
                    QuickFacts = t.QuickFacts
                }).FirstOrDefault();

                ViewBag.Place = place;
            }


            
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

    public class CovGuid
    {
        public string Id { get; set; }
        public string PlaceName { get; set; }
    }


    public class VMPlaceInHome
    {
        public Guid Id { get; set; }
        public string PlaceName { get; set; }
        public long Longitude { get; set; }
        public long Latitude { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string QuickFacts { get; set; }

    }



}
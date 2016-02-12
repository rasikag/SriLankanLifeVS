using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SriLankanLifeVS.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        // This will work as admin dashboard 
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SliderImageManage()
        {
            return View();
        }

        // add or remove district 

        public ActionResult DistrictManage()
        {
            return View();
        }

        //add or remove town 

        public ActionResult TownManage()
        {
            return View();
        }

        // Place category manage 

        public ActionResult PlaceCategoryManage()
        {
            return View();
        }

        // Place Manage

        public ActionResult PlaceManage()
        {
            return View();
        }

        // Place image manage admin

        public ActionResult PlaceAdminImageManage()
        {
            return View();
        }

        // Event category manage

        public ActionResult EventCategoryManage()
        {
            return View();
        }

        public ActionResult EventManage()
        {
            return View();
        }

        public ActionResult EventAdminImageManage()
        {
            return View();
        }


    }
}
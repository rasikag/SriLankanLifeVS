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



    }
}
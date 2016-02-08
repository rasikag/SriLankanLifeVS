using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SriLankanLifeVS.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddCommentByUser()
        {
            return View();
        }

        public ActionResult AddVideoByUserEvent()
        {
            return View();
        }

        public ActionResult AddImageByUserEvent()
        {
            return View();
        }

        public ActionResult EventInDetails()
        {
            return View();
        }


    }
}
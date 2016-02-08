using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http;
using SriLankanLifeVS.Models.EntityModels;
using SriLankanLifeVS.Models.TravelContext;

namespace SriLankanLifeVS.Controllers
{
    public class EventVideoManageController : ApiController
    {

        private TravelContext _db = new TravelContext();

        [HttpPost]
        [Route("api/add-event-video-by-user")]
        public IHttpActionResult AddVideoURL(Video video)
        {
            try
            {
                EventVideoUser pcu = new EventVideoUser();

                pcu.Active = false;
                pcu.Id = Guid.NewGuid();
                pcu.AddedDate = DateTime.Today;
                pcu.EventId = Guid.Parse("E7F271A2-2190-4E1C-84F9-4641C1A7D9AD");
                pcu.VideoURL = video.VideoURL;
                pcu.Email = video.Email;
                pcu.UserName = video.Name;

                _db.EventVideosUser.Add(pcu);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

    }
}

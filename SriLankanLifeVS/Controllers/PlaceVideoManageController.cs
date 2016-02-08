using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SriLankanLifeVS.Models.EntityModels;
using SriLankanLifeVS.Models.TravelContext;

namespace SriLankanLifeVS.Controllers
{
    public class PlaceVideoManageController : ApiController
    {
        private TravelContext _db = new TravelContext();

        [HttpPost]
        [Route("api/add-place-video-by-user")]
        public IHttpActionResult AddVideoURL(Video video)
        {
            try
            {
                PlaceVideoUser pcu = new PlaceVideoUser();

                pcu.Active = false;
                pcu.Id = Guid.NewGuid();
                pcu.AddedDate = DateTime.Today;
                pcu.PlaceId = Guid.Parse("94F9EB7F-EB00-4267-922C-EB7BE4D6EC9A");
                pcu.VideoURL = video.VideoURL;
                pcu.Email = video.Email;
                pcu.UserName = video.Name;

                _db.PlaceVideosUser.Add(pcu);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }



    }

    public class Video
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string VideoURL { get; set; }
    }

}

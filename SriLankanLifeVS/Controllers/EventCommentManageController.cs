using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ComponentModel.DataAnnotations;
using SriLankanLifeVS.Models.TravelContext;
using SriLankanLifeVS.Models.EntityModels;


namespace SriLankanLifeVS.Controllers
{
    public class EventCommentManageController : ApiController
    {

        TravelContext _db = new TravelContext();

        [HttpPost]
        [Route("api/submit-comment-for-event")]
        public IHttpActionResult SubmitComment(Comment Comment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    EventCommentUser ecu = new EventCommentUser();
                    ecu.Id = Guid.NewGuid();
                    ecu.Active = false;
                    ecu.AddedDate = DateTime.Today;
                    ecu.Comment = Comment.CommentData;
                    ecu.Email = Comment.Email;
                    ecu.EventId = Guid.Parse("596C8EF2-2E64-4329-8EE1-BD53756E3D8C");
                    ecu.UserName = Comment.Name;
                    _db.EventCommentsUser.Add(ecu);
                    _db.SaveChanges();
                    return Ok();

                }
                catch (Exception ex)
                {
                    return BadRequest("Bad Request");
                }
            }
            return BadRequest("Bad Request");
        }
    }
}

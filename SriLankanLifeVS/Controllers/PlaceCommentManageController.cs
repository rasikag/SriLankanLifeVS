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
    public class PlaceCommentManageController : ApiController
    {
        TravelContext _db = new TravelContext();

        [HttpPost]
        [Route("api/submit-comment")]
        public IHttpActionResult SubmitComment(Comment Comment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    PlaceCommentUser pcu = new PlaceCommentUser();
                    pcu.Id = Guid.NewGuid();
                    pcu.UserName = Comment.Name;
                    pcu.Email = Comment.Email;
                    pcu.Comment = Comment.CommentData;
                    pcu.Active = false;
                    pcu.AddedDate = DateTime.Today;
                    pcu.PlaceId = Guid.Parse("94F9EB7F-EB00-4267-922C-EB7BE4D6EC9A");
                    _db.PlaceCommentUser.Add(pcu);
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

    public class Comment
    {   
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string CommentData { get; set; }
    }


}

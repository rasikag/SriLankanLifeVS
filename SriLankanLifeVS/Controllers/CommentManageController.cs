using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SriLankanLifeVS.Controllers
{
    public class CommentManageController : ApiController
    {

        [HttpPost]
        [Route("api/submit-comment")]
        public IHttpActionResult SubmitComment(Comment Comment)
        {
            return Ok();
        } 

    }

    public class Comment
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CommentData { get; set; }
    }


}

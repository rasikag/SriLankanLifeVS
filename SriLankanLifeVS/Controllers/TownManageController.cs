using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SriLankanLifeVS.Models.TravelContext;
using SriLankanLifeVS.Models.EntityModels;

namespace SriLankanLifeVS.Controllers
{
    public class TownManageController : ApiController
    {
        private TravelContext _db = new TravelContext();

        public async Task<IHttpActionResult> GetAll()
        {
            return Ok();
        }

        [HttpGet]
        [Route("api/get-by-name")]
        public IQueryable<District> GetDistrictByName(string Name)
        {
            
            IQueryable<District> dist = _db.Districts.Where(p => p.DistrictName.Contains(Name));
            
            return dist;
        }
    }
}

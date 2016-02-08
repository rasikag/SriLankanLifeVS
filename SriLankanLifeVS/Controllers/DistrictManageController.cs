using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.ComponentModel.DataAnnotations;
using SriLankanLifeVS.Models.TravelContext;
using SriLankanLifeVS.Models.EntityModels;

namespace SriLankanLifeVS.Controllers
{
    public class DistrictManageController : ApiController
    {

        private TravelContext _db = new TravelContext();

        [HttpGet]
        [Route("api/get-all-district")]
        public IHttpActionResult GetAllDistrict()
        {
            List<District> d =  _db.Districts.ToList();

            return Ok(d);
        }

        [HttpPost]
        [Route("api/add-district")]
        public async Task<IHttpActionResult> AddDistrict(DistrictVM district)
        {
            if (ModelState.IsValid)
            {
                District _district = new District();
                _district.DistrictName = district.DistrictName;
                _db.Districts.Add(_district);
                await _db.SaveChangesAsync();
                return Ok("Successfully added to DataBase");
            }
            else
            {
                return BadRequest("Internal Error");
            }

            
        }


    }

    // view models 

    public class DistrictVM
    {
        [Required]
        public string DistrictName { get; set; }
    }

}

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


        [HttpPost]
        [Route("api/edit-district")]
        public async Task<IHttpActionResult> EditDistrict(DistrictEdit dist)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    District district = _db.Districts.SingleOrDefault(x => x.Id == dist.Id);
                    if(district != null)
                    {
                        district.DistrictName = dist.DistrictName;
                        await _db.SaveChangesAsync();
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Cann't find distict");
                    }
                    
                }
                catch (Exception e)
                {
                    return BadRequest("District name could not chage. Please try again");
                }
            }
            else
            {
                return BadRequest("Model is not valid");
            }
        }


        [HttpPost]
        [Route("api/delete-district")]
        public async Task<IHttpActionResult> DeleteDistrict(DistrictEdit dist)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    District district = _db.Districts.SingleOrDefault(x => x.Id == dist.Id);
                    if (district != null)
                    {
                        //district.DistrictName = dist.DistrictName;
                        _db.Districts.Remove(district);
                        await _db.SaveChangesAsync();
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Cann't find distict");
                    }

                }
                catch (Exception e)
                {
                    return BadRequest("District name could not chage. Please try again");
                }
            }
            else
            {
                return BadRequest("Model is not valid");
            }

        }


    }

    // view models 

    public class DistrictVM
    {
        [Required]
        public string DistrictName { get; set; }
    }

    public class DistrictEdit
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string DistrictName { get; set; }

    }

}

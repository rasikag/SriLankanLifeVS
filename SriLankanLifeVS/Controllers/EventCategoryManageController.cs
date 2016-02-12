using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using SriLankanLifeVS.Models.EntityModels;
using SriLankanLifeVS.Models.TravelContext;

namespace SriLankanLifeVS.Controllers
{
    public class EventCategoryManageController : ApiController
    {
        private TravelContext _db = new TravelContext();

        [HttpGet]
        [Route("api/get-all-event-category")]
        public IHttpActionResult GetAllDistrict()
        {
            List<EventCategory> d = _db.EventCategories.ToList();
            return Ok(d);
        }

        [HttpPost]
        [Route("api/add-event-categoty")]
        public async Task<IHttpActionResult> AddDistrict(VMPlaceCategory cat)
        {
            if (ModelState.IsValid)
            {
                EventCategory _district = new EventCategory();
                _district.CategotyName = cat.CategoryName;
                _db.EventCategories.Add(_district);
                await _db.SaveChangesAsync();
                return Ok("Successfully added to DataBase");
            }
            else
            {
                return BadRequest("Internal Error");
            }


        }


        [HttpPost]
        [Route("api/edit-event-category")]
        public async Task<IHttpActionResult> EditDistrict(VMPlaceCategory dist)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    EventCategory district = _db.EventCategories.SingleOrDefault(x => x.Id == dist.Id);
                    if (district != null)
                    {
                        district.CategotyName = dist.CategoryName;
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
        [Route("api/delete-event-category")]
        public async Task<IHttpActionResult> DeleteDistrict(VMPlaceCategory dist)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    EventCategory district = _db.EventCategories.SingleOrDefault(x => x.Id == dist.Id);
                    if (district != null)
                    {
                        //district.DistrictName = dist.DistrictName;
                        _db.EventCategories.Remove(district);
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
}

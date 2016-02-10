using SriLankanLifeVS.Models.EntityModels;
using SriLankanLifeVS.Models.TravelContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SriLankanLifeVS.Controllers
{
    public class PlaceCategoryManageController : ApiController
    {
        private TravelContext _db = new TravelContext();

        [HttpGet]
        [Route("api/get-all-place-category")]
        public IHttpActionResult GetAllDistrict()
        {
            List<PlaceCategory> d = _db.PlaceCategories.ToList();

            return Ok(d);
        }

        [HttpPost]
        [Route("api/add-place-categoty")]
        public async Task<IHttpActionResult> AddDistrict(VMPlaceCategory cat)
        {
            if (ModelState.IsValid)
            {
                PlaceCategory _district = new PlaceCategory();
                _district.CategoryName = cat.CategoryName;
                _db.PlaceCategories.Add(_district);
                await _db.SaveChangesAsync();
                return Ok("Successfully added to DataBase");
            }
            else
            {
                return BadRequest("Internal Error");
            }


        }


        [HttpPost]
        [Route("api/edit-place-category")]
        public async Task<IHttpActionResult> EditDistrict(VMPlaceCategory dist)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    PlaceCategory district = _db.PlaceCategories.SingleOrDefault(x => x.Id == dist.Id);
                    if (district != null)
                    {
                        district.CategoryName = dist.CategoryName;
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
        [Route("api/delete-place-category")]
        public async Task<IHttpActionResult> DeleteDistrict(VMPlaceCategory dist)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    PlaceCategory district = _db.PlaceCategories.SingleOrDefault(x => x.Id == dist.Id);
                    if (district != null)
                    {
                        //district.DistrictName = dist.DistrictName;
                        _db.PlaceCategories.Remove(district);
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

    public class VMPlaceCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}

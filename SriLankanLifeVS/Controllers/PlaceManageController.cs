using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using SriLankanLifeVS.Models.TravelContext;
using SriLankanLifeVS.Models.EntityModels;
using System.ComponentModel.DataAnnotations;

namespace SriLankanLifeVS.Controllers
{
    public class PlaceManageController : ApiController
    {

        private TravelContext _db = new TravelContext();

        [HttpGet]
        [Route("api/get-town-by-name-place")]
        public IQueryable GetTownByName(string Name)
        {

            var twn = _db.Towns.Where(p => p.TownName.Contains(Name)).Select(t => new { Id = t.Id, TownName = t.TownName });

            return twn;
        }


        [HttpPost]
        [Route("api/add-place")]
        public async Task<IHttpActionResult> AddPlace(VMPlace Plc)
        {
            if (ModelState.IsValid)
            {
                Town t = _db.Towns.FirstOrDefault(dis => dis.TownName == Plc.TownName);
                if (t == null)
                {
                    return BadRequest("Town is not difined. Please add a difined town that suggest by the list");
                }

                Place place = new Place();
                place.Id = Guid.NewGuid();
                place.Address = Plc.Address;
                place.Description = Plc.Discription;
                place.Latitude = Plc.Latitude;
                place.Longitude = Plc.Longitude;
                place.PlaceName = Plc.PlaceName;
                place.QuickFacts = Plc.QFacts;
                place.TownId = t.Id;
                _db.Places.Add(place);
                await _db.SaveChangesAsync();


                return Ok("Successfully added recode");
            }
            else
            {
                return BadRequest("Model state is not valid");
            }
        }

        [HttpGet]
        [Route("api/get-all-places")]
        public IHttpActionResult GetAllPlaces()
        {
            var a = _db.Places.Select(t => new
            {
                Address = t.Address,
                Description = t.Description,
                Id = t.Id,
                QuickFacts = t.QuickFacts,
                Latitude = t.Latitude,
                Longitude = t.Longitude,
                PlaceName = t.PlaceName,
                TownId = t.TownId,
                TownName = _db.Towns.Where(q => q.Id == t.TownId).Select(r => r.TownName)
            });

            return Ok(a);
        }


        [HttpPost]
        [Route("api/edit-place")]
        public IHttpActionResult EditPlace(VMPlace plc)
        {
            if (ModelState.IsValid)
            {
                Town t = _db.Towns.FirstOrDefault(dis => dis.TownName == plc.TownName);
                if (t != null)
                {
                    Guid guid = Guid.Parse(plc.Id);
                    Place p = _db.Places.FirstOrDefault(pl => pl.Id == guid);
                    if (p!=null)
                    {
                        p.Address = plc.Address;
                        p.Description = plc.Discription;
                        p.Latitude = plc.Latitude;
                        p.Longitude = plc.Longitude;
                        p.PlaceName = plc.PlaceName;
                        p.QuickFacts = plc.QFacts;
                        p.TownId = t.Id;
                        _db.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Recode update fail. Please try again");
                    }
                }
                else
                {
                    return BadRequest("Recode update fail. Please try again");
                }
            }
            else
            {
                return BadRequest("Recode update fail. Please try again");
            }
        }

        //[FromBody]
        //string Id
        [HttpPost]
        [Route("api/delete-place")]
        public IHttpActionResult DeletePlace(VMPlace vmp)
        {
            if (vmp.Id!=null )
            {
                Guid _guid = Guid.Parse(vmp.Id);
                Place p = _db.Places.FirstOrDefault( plc => plc.Id == _guid);
                if (p!=null)
                {
                    _db.Places.Remove(p);
                    _db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }



    }
    //h= new { Id = t.TownId,
    //TownName = _db.Towns.Select(p => p.TownName).Where()   }

    public class VMPlace
    {
        public string Id { get; set; }
        [Required]
        public string PlaceName { get; set; }
        [Required]
        public long Longitude { get; set; }
        [Required]
        public long Latitude { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Discription { get; set; }
        [Required]
        public string QFacts { get; set; }
        [Required]
        public string TownName { get; set; }

    }


}

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
        public IQueryable GetDistrictByName(string Name)
        {

            var twn = _db.Towns.Where(p => p.TownName.Contains(Name)).Select(t=> new { Id = t.Id , TownName = t.TownName });

            return twn;
        }


        [HttpPost]
        [Route("api/add-place")]
        public async Task<IHttpActionResult> AddTown(VMPlace Plc)
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

        [HttpPost]
        [Route("api/get-all-places")]
        public void GetAllPlaces()
        {

        }

    }


    public class VMPlace
    {
        public Guid Id { get; set; }
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

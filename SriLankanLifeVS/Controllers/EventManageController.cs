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
using Newtonsoft.Json;

namespace SriLankanLifeVS.Controllers
{
    public class EventManageController : ApiController
    {
        private TravelContext _db = new TravelContext();

        [HttpGet]
        [Route("api/get-town-by-name-event")]
        public IQueryable GetTownByName(string Name)
        {

            var twn = _db.Towns.Where(p => p.TownName.Contains(Name)).Select(t => new { Id = t.Id, TownName = t.TownName });

            return twn;
        }


        [HttpPost]
        [Route("api/add-event")]
        public async Task<IHttpActionResult> AddPlace(VMPlace Plc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model state is not valid");
            }
            

            Town t = _db.Towns.FirstOrDefault(dis => dis.TownName == Plc.TownName);
            if (t == null)
            {
                return BadRequest("Town is not difined. Please add a difined town that suggest by the list");
            }

            string[] strings = JsonConvert.DeserializeObject<string[]>(Plc.CategoryName);

            List<EventCategory> cta = new List<EventCategory>();


            if (strings != null)
            {
                for (int i = 0; i < strings.Count(); i++)
                {
                    string n = strings[i];
                    //.Select(g => new CatId { Id = t.Id })
                    EventCategory c = _db.EventCategories.Where(cat => cat.CategotyName == n).FirstOrDefault();
                    if (c == null)
                    {

                        return BadRequest("Category is not define. Please add correct category");
                    }

                    cta.Add(c);
                }
            }

            Event place = new Event();
            place.Id = Guid.NewGuid();
            place.Address = Plc.Address;
            place.Description = Plc.Discription;
            place.Latitude = Plc.Latitude;
            place.Longitude = Plc.Longitude;
            place.EventName = Plc.PlaceName;
            place.QuickFacts = Plc.QFacts;
            place.TownId = t.Id;
            place.EventCategories = cta;
            _db.Events.Add(place);
            await _db.SaveChangesAsync();


            return Ok("Successfully added recode");

        }

        [HttpGet]
        [Route("api/get-all-evnts")]
        public IHttpActionResult GetAllPlaces()
        {
            var a = _db.Events.Select(t => new
            {
                Address = t.Address,
                Description = t.Description,
                Id = t.Id,
                QuickFacts = t.QuickFacts,
                Latitude = t.Latitude,
                Longitude = t.Longitude,
                PlaceName = t.EventName,
                TownId = t.TownId,
                TownName = _db.Towns.Where(q => q.Id == t.TownId).Select(r => r.TownName)
            });

            return Ok(a);
        }


        [HttpPost]
        [Route("api/edit-event")]
        public IHttpActionResult EditPlace(VMPlace plc)
        {
            if (ModelState.IsValid)
            {
                Town t = _db.Towns.FirstOrDefault(dis => dis.TownName == plc.TownName);
                if (t != null)
                {
                    Guid guid = Guid.Parse(plc.Id);
                    Event p = _db.Events.FirstOrDefault(pl => pl.Id == guid);
                    if (p != null)
                    {
                        p.Address = plc.Address;
                        p.Description = plc.Discription;
                        p.Latitude = plc.Latitude;
                        p.Longitude = plc.Longitude;
                        p.EventName = plc.PlaceName;
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
        [Route("api/delete-event")]
        public IHttpActionResult DeletePlace(VMPlace vmp)
        {
            if (vmp.Id != null)
            {
                Guid _guid = Guid.Parse(vmp.Id);
                Event p = _db.Events.FirstOrDefault(plc => plc.Id == _guid);
                if (p != null)
                {
                    _db.Events.Remove(p);
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


        [HttpGet]
        [Route("api/get-event-categories-in-event")]
        public IHttpActionResult GetPlaceCategories(string Name)
        {
            var twn = _db.EventCategories.Where(p => p.CategotyName.Contains(Name)).Select(t => new { Id = t.Id, CategoryName = t.CategotyName });

            return Ok(twn);
        }

    }
}

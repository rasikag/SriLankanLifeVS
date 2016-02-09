using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SriLankanLifeVS.Models.TravelContext;
using SriLankanLifeVS.Models.EntityModels;
using System.ComponentModel.DataAnnotations;
using SriLankanLifeVS.Models.EntityModels;

namespace SriLankanLifeVS.Controllers
{
    public class TownManageController : ApiController
    {
        private TravelContext _db = new TravelContext();

        [HttpGet]
        [Route("api/get-all")]
        public IHttpActionResult GetAll()
        {
            List<Town> town = _db.Towns.ToList();
            //List<VMTownGetAll> townAll = new List<VMTownGetAll>();
            //for (int i = 0; i<town.Capacity; i++)
            //{
            //    VMTownGetAll _town = new VMTownGetAll();
            //    _town.TownId = town[i].Id;
            //    _town.TownName = town[i].TownName;
            //    _town.DistrictId = town[i].DistrictId;
            //    _town.DistrictName = town[i].District.DistrictName;
            //    townAll.Add(_town);
            //}
            return Ok(town);
        }

        [HttpGet]
        [Route("api/get-by-name")]
        public IQueryable<District> GetDistrictByName(string Name)
        {

            IQueryable<District> dist = _db.Districts.Where(p => p.DistrictName.Contains(Name));

            return dist;
        }

        [HttpPost]
        [Route("api/add-town")]
        public async Task<IHttpActionResult> AddTown(VMTown Town)
        {
            if (ModelState.IsValid)
            {
                District d = _db.Districts.FirstOrDefault(dis => dis.DistrictName == Town.DistrictName);
                if (d == null)
                {
                    return BadRequest("DIstrict is not difined. Please add a difined district that suggest by the list");
                }

                Town t = new Town();
                t.DistrictId = d.Id;
                t.TownName = Town.TownName;
                _db.Towns.Add(t);
                await _db.SaveChangesAsync();
                return Ok("Successfully added recode");
            }
            else
            {
                return BadRequest("Model state is not valid");
            }
        }


        [HttpPost]
        [Route("api/edit-town")]
        public async Task<IHttpActionResult> EditTown(VMTownGetAll t)
        {
            District d = _db.Districts.FirstOrDefault(dis => dis.DistrictName == t.DistrictName);
            if (d == null)
            {
                return BadRequest("DIstrict is not difined. Please add a difined district that suggest by the list");
            }

            Town town = _db.Towns.FirstOrDefault(twn => twn.Id == t.TownId);
            if (town == null)
            {
                return BadRequest("Error happend. Please try again");
            }

            town.DistrictId = d.Id;
            town.TownName = t.TownName;
            await _db.SaveChangesAsync();
            return Ok("Succesfully added");
        }

        [HttpPost]
        [Route("api/delete-town")]
        public async Task<IHttpActionResult> DeleteTown([FromBody] int Id)
        {

            Town town = _db.Towns.FirstOrDefault(twn => twn.Id == Id);
            _db.Towns.Remove(town);
            await _db.SaveChangesAsync();
            return Ok();
        }

    }

    public class VMTown
    {
        [Required]
        public string TownName { get; set; }
        [Required]
        public string DistrictName { get; set; }
    }


    public class VMTownGetAll
    {
        public int TownId { get; set; }
        public string TownName { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
    }

}

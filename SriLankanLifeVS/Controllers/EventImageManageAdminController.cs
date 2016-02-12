using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using SriLankanLifeVS.Extensions;
using SriLankanLifeVS.Models;
using SriLankanLifeVS.Models.EntityModels;
using SriLankanLifeVS.Models.TravelContext;
using System.Web;
using System.IO;

namespace SriLankanLifeVS.Controllers
{
    public class EventImageManageAdminController : ApiController
    {
        private readonly string workingFolder = HttpRuntime.AppDomainAppPath + @"\Images\AdminUploadedEvents";
        private TravelContext _db = new TravelContext();

        [HttpPost]
        [Route("api/add-event-image-by-admin")]
        public async Task<IHttpActionResult> AddImageByUserPlace()
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return BadRequest("Unsupported media type");
            }
            try
            {
                var provider = new CustomMultipartFormDataStreamProvider(workingFolder);

                await Request.Content.ReadAsMultipartAsync(provider);

                string PlaceName = null;

                foreach (var key in provider.FormData.AllKeys)
                {
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        if (key == "PlaceName")
                        {
                            PlaceName = val;
                        }
                    }
                }

                if (PlaceName != null)
                {
                    IQueryable<Guid> a = _db.Events.Where(p => p.EventName == PlaceName).Select(t => t.Id);
                    if (a != null)
                    {
                        string fileExtension = null;
                        string fileName = null;
                        long? fileSize = null;

                        var getExtension = provider.FileData
                            .Select(file => new FileInfo(file.LocalFileName)).Select(fileInfo => new FileData
                            {
                                FileName = fileInfo.Name,
                                FileExtension = fileInfo.Extension,
                                FileSize = fileInfo.Length
                            });

                        foreach (var item in getExtension)
                        {
                            fileExtension = item.FileExtension;
                            fileName = item.FileName;
                            fileSize = item.FileSize;
                        }

                        string newName = Guid.NewGuid().ToString();

                        try
                        {
                            if (File.Exists(workingFolder + "/" + fileName))
                            {
                                File.Move(workingFolder + "/" + fileName, workingFolder + "/" + newName + fileExtension);
                                EventImageAdmin piu = new EventImageAdmin();
                                piu.Active = false;
                                piu.Addeddate = DateTime.Today;
                                piu.FileSize = (long)fileSize / 1024;
                                piu.Id = Guid.Parse(newName);
                                piu.ImageExtention = fileExtension;
                                piu.EventId = a.First();
                                _db.EventImagesAdmin.Add(piu);
                                await _db.SaveChangesAsync();
                                return Ok("Added Succesfully");

                            }
                            else
                            {
                                return BadRequest("Image Upload Fail");
                            }

                        }
                        catch (Exception ex)
                        {
                            return BadRequest(ex.ToString());
                        }
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
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);

            }
        }




        [HttpGet]
        [Route("api/get-event-by-name-admin-image")]
        public IHttpActionResult GetAllPlaces(string Name)
        {
            var place = _db.Events.Where(p => p.EventName.Contains(Name)).Select(t => new { PlaceName = t.EventName });
            return Ok(place);
        }

        [HttpGet]
        [Route("api/get-all-images-admin-event")]
        public IHttpActionResult GetAllImages()
        {
            var a = _db.EventImagesAdmin.Select(t => new { Id = t.Id, ImageName = t.Id + t.ImageExtention, Active = t.Active });

            return Ok(a);
        }

        [HttpPost]
        [Route("api/delete-event-image-admin")]
        public async Task<IHttpActionResult> DeleteImage(DeleteModel dm)
        {
            if (!FileExists(dm.fileName))
            {
                return NotFound();
            }

            try
            {
                var filePath = Directory.GetFiles(workingFolder, dm.fileName)
                  .FirstOrDefault();

                await Task.Factory.StartNew(() =>
                {
                    if (filePath != null)
                        File.Delete(filePath);
                });

                Guid imgId = Guid.Parse(dm.Id);

                var image = _db.EventImagesAdmin.SingleOrDefault(x => x.Id == imgId);
                if (image != null)
                {
                    _db.EventImagesAdmin.Remove(image);
                    _db.SaveChanges();
                }

                return Ok("Deleted");
            }
            catch (Exception ex)
            {

                return BadRequest("Not deleted");
            }
        }

        [HttpPost]
        [Route("api/change-active-image-event")]
        public async Task<IHttpActionResult> ChangeActiveImage(ChangeImageState test)
        {
            try
            {
                Guid id = Guid.Parse(test.Id);

                var image = _db.EventImagesAdmin.SingleOrDefault(x => x.Id == id);
                if (image != null)
                {
                    image.Active = test.Active;
                    await _db.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }



        }

        public bool FileExists(string fileName)
        {
            var file = Directory.GetFiles(workingFolder, fileName)
              .FirstOrDefault();

            return file != null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using SriLankanLifeVS.Extensions;
using SriLankanLifeVS.Models;
using SriLankanLifeVS.Models.EntityModels;
using SriLankanLifeVS.Models.TravelContext;
using System.Web;

namespace SriLankanLifeVS.Controllers
{
    public class PlaceImageManageAdminController : ApiController
    {
        private readonly string workingFolder = HttpRuntime.AppDomainAppPath + @"\Images\AdminUploadedPlaces";
        private TravelContext _db = new TravelContext();

        [HttpPost]
        [Route("api/add-place-image-by-admin")]
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

                if (PlaceName!=null)
                {
                    IQueryable<Guid> a = _db.Places.Where(p => p.PlaceName == PlaceName).Select(t=>t.Id);
                    if (a!=null)
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
                                PlaceImageAdmin piu = new PlaceImageAdmin();
                                piu.Active = false;
                                piu.AddedDate = DateTime.Today;
                                piu.FileSize = (long)fileSize / 1024;
                                piu.Id = Guid.Parse(newName);
                                piu.ImageExtention = fileExtension;
                                piu.PlaceId = a.First();
                                _db.PlaceImagesAdmin.Add(piu);
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

        //[HttpPost]
        //[Route("api/update-user-data")]
        //public async Task<IHttpActionResult> UpdateUserDetail(UserData user)
        //{
        //    if (user.Id != null)
        //    {
        //        Guid Id = Guid.Parse(user.Id);

        //        PlaceImageUser piu = _db.PlaceImagesUser.FirstOrDefault(u => u.Id == Id);

        //        if (piu != null)
        //        {
        //            piu.Email = user.Email;
        //            piu.UserName = user.Name;
        //            await _db.SaveChangesAsync();
        //            return Ok("DataBase Updated Successfully");
        //        }

        //    }
        //    else
        //    {

        //    }

        //    return Ok();

        //}


        [HttpGet]
        [Route("api/get-places-by-name-admin-image")]
        public IHttpActionResult GetAllPlaces(string Name)
        {
            var place = _db.Places.Where(p => p.PlaceName.Contains(Name)).Select(t=>new { PlaceName = t.PlaceName  });
            return Ok(place);
        }

        [HttpGet]
        [Route("api/get-all-images-admin-palce")]
        public IHttpActionResult GetAllImages()
        {
            var a = _db.PlaceImagesAdmin.Select(t=>new { Id = t.Id ,ImageName =t.Id + t.ImageExtention , Active = t.Active});

            return Ok(a);
        }

        [HttpPost]
        [Route("api/delete-place-image-admin")]
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

                var image = _db.PlaceImagesAdmin.SingleOrDefault(x => x.Id == imgId );
                if (image != null)
                {
                    _db.PlaceImagesAdmin.Remove(image);
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
        [Route("api/change-active-image")]
        public async Task<IHttpActionResult> ChangeActiveImage(ChangeImageState test)
        {
            try
            {
                Guid id = Guid.Parse(test.Id);

                var image = _db.PlaceImagesAdmin.SingleOrDefault(x => x.Id == id);
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


    public class VMPlaceName{

        public string Name { get; set; }
    }

    public class DeleteModel
    {
        public string fileName { get; set; }
        public string Id { get; set; }
    }

    public class ChangeImageState
    {
        public string Id { get; set; }
        public bool Active { get; set; }
    }

}

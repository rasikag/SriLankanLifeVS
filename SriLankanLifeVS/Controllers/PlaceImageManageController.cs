using SriLankanLifeVS.Extensions;
using SriLankanLifeVS.Models;
using SriLankanLifeVS.Models.EntityModels;
using SriLankanLifeVS.Models.TravelContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SriLankanLifeVS.Controllers
{
    public class PlaceImageManageController : ApiController
    {
        private readonly string workingFolder = HttpRuntime.AppDomainAppPath + @"\Images\UserUploadedPlaces";
        private TravelContext _db = new TravelContext();

        [HttpPost]
        [Route("api/add-place-image-by-user")]
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

                //string extension = Path.GetExtension(fileData.FileName);

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
                        PlaceImageUser piu = new PlaceImageUser();
                        piu.Active = false;
                        piu.AddedDate = DateTime.Today;
                        piu.FileSize = (long)fileSize / 1024;
                        piu.Id = Guid.Parse(newName);
                        piu.ImageExtention = fileExtension;
                        piu.PlaceId = Guid.Parse("98535D67-8CF8-473C-93D7-9A23033D336F");
                        _db.PlaceImagesUser.Add(piu);
                        await _db.SaveChangesAsync();

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


                return Ok(newName);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);

            }
        }

        [HttpPost]
        [Route("api/update-user-data")]
        public async Task<IHttpActionResult> UpdateUserDetail(UserData user)
        {
            if (user.Id != null)
            {
                Guid Id = Guid.Parse(user.Id);

                PlaceImageUser piu = _db.PlaceImagesUser.FirstOrDefault(u => u.Id == Id);

                if (piu != null)
                {
                    piu.Email = user.Email;
                    piu.UserName = user.Name;
                    await _db.SaveChangesAsync();
                    return Ok("DataBase Updated Successfully");
                }

            }
            else
            {
                
            }

            return Ok();

        }


    }

    public class UserData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
    }
}

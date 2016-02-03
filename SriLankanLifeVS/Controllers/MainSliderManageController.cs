using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web;
using SriLankanLifeVS.Models.EntityModels;
using System.IO;
using SriLankanLifeVS.ViewModels;
using SriLankanLifeVS.Extensions;
using SriLankanLifeVS.Models;
using SriLankanLifeVS.Models.TravelContext;

namespace SriLankanLifeVS.Controllers
{
    public class MainSliderManageController : ApiController
    {
        private readonly string workingFolder = HttpRuntime.AppDomainAppPath + @"\Images\FullScreenSlider";
        private TravelContext _dbContext = new TravelContext();


        public IHttpActionResult Get()
        {
            List<SliderImage> si = _dbContext.SliderImages.ToList();

            return Ok(new { Photos = si });

        }


        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string fileName, int Id)
        {
            if (!FileExists(fileName))
            {
                return NotFound();
            }

            try
            {
                var filePath = Directory.GetFiles(workingFolder, fileName)
                  .FirstOrDefault();

                await Task.Factory.StartNew(() =>
                {
                    if (filePath != null)
                        File.Delete(filePath);
                });

                var result = new PhotoActionResult
                {
                    Successful = true,
                    Message = fileName + "deleted successfully"
                };
                var image = _dbContext.SliderImages.SingleOrDefault(x => x.Id == Id);
                if (image != null)
                {
                    _dbContext.SliderImages.Remove(image);
                    _dbContext.SaveChanges();
                }

                return Ok(new { message = result.Message });
            }
            catch (Exception ex)
            {
                var result = new PhotoActionResult
                {
                    Successful = false,
                    Message = "error deleting fileName " + ex.GetBaseException().Message
                };
                return BadRequest(result.Message);
            }
        }



        [HttpPost]
        [Route("api/add-image-to-main-slider")]
        public async Task<IHttpActionResult> Post()
        {
            // Check if the request contains multipart/form-data.
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
                        SliderImage sliderImage = new SliderImage();
                        sliderImage.ImageName = newName + fileExtension;
                        sliderImage.Active = false;
                        sliderImage.AddedDate = DateTime.Today.ToString("d");
                        sliderImage.Size = (long)fileSize / 1024;
                        _dbContext.SliderImages.Add(sliderImage);
                        await _dbContext.SaveChangesAsync();
                        //return Ok(new { Message = "Photos uploaded ok" });
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.GetBaseException().Message);
                }


                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);

            }
        }

        [HttpPost]
        [Route("api/changeactiveimage")]
        public async Task<IHttpActionResult> ChangeActiveImage(Test test)
        {
            try
            {
                var image = _dbContext.SliderImages.SingleOrDefault(x => x.Id == test.Id);
                if (image != null)
                {
                    image.Active = test.Active;
                    await _dbContext.SaveChangesAsync();
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


    public class Test
    {
        public int Id { get; set; }
        public bool Active { get; set; }
    }
}

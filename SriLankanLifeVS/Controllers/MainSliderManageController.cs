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

namespace SriLankanLifeVS.Controllers
{
    public class MainSliderManageController : ApiController
    {
        private readonly string workingFolder = HttpRuntime.AppDomainAppPath + @"\Images\FullScreenSlider";

        /// <summary>
        ///   Get all photos
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get()
        {
            var photos = new List<SliderImageViewModel>();

            var photoFolder = new DirectoryInfo(workingFolder);

            await Task.Factory.StartNew(() =>
            {
                photos = photoFolder.EnumerateFiles()
                  .Where(fi => new[] { ".jpg", ".bmp", ".png", ".gif", ".tiff" }
                    .Contains(fi.Extension.ToLower()))
                  .Select(fi => new SliderImageViewModel
                  {
                      Name = fi.Name,
                      Size = fi.Length / 1024
                  })
                  .ToList();
            });

            return Ok(new { Photos = photos });
        }

        /// <summary>
        ///   Delete photo
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> Delete([FromBody]string fileName)
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

        /// <summary>
        ///   Add a photo
        /// </summary>
        /// <returns></returns>
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

                var getExtension = provider.FileData
                    .Select(file => new FileInfo(file.LocalFileName)).Select(fileInfo => new FileData
                    {
                        FileName = fileInfo.Name,
                        FileExtension = fileInfo.Extension
                    });

                foreach (var item in getExtension)
                {
                    fileExtension = item.FileExtension;
                    fileName = item.FileName;
                }
                string newName = Guid.NewGuid().ToString();
                if (File.Exists(workingFolder + "/" + fileName))
                {
                    File.Move(workingFolder + "/" + fileName, workingFolder + "/" + newName + fileExtension);
                }
                else
                {
                    string a = workingFolder + "/" + fileName + fileExtension;
                }

                var newProvider = new CustomMultipartFormDataStreamProvider(workingFolder);

                var photos =
                  newProvider.FileData
                    .Select(file => new FileInfo(newName + fileExtension))
                    .Select(fileInfo => new SliderImageViewModel
                    {
                        Name = fileInfo.Name,
                        Size = fileInfo.Length / 1024
                    }).ToList();

                return Ok(new { Message = "Photos uploaded ok", Photos = photos });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        /// <summary>
        ///   Check if file exists on disk
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool FileExists(string fileName)
        {
            var file = Directory.GetFiles(workingFolder, fileName)
              .FirstOrDefault();

            return file != null;
        }

    }
}

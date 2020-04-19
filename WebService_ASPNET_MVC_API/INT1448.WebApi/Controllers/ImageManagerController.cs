using INT1448.Application.Infrastructure.DTOs;
using INT1448.Application.IServices;
using INT1448.Application.Storage;
using INT1448.Shared.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Linq;

namespace INT1448.WebApi.Controllers
{
    [RoutePrefix("images")]
    public class ImageManagerController : ApiController
    {
        private IStorageService _storageService;
        private IBookImageManagerService _bookImageManagerService;

        public ImageManagerController(IStorageService storageService, IBookImageManagerService bookImageManagerService)
        {
            _storageService = storageService;
            _bookImageManagerService = bookImageManagerService;
        }

        [Route("bookimage/{*filename}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetImage(string fileName)
        {
            IEnumerable<BookImageDTO> bookImages = await _bookImageManagerService.GetAll();

            IEnumerable<BookImageDTO> bookMatch =  bookImages.Where(x => 
                             (x.ImagePath.Substring(x.ImagePath.LastIndexOf("/") + 1).Equals(fileName))
                             );
            if (bookMatch.Count() == 0)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, new string[] { "Success: false", "Message: Can not find your file!" });
            }

            HttpResponseMessage response = this.Request.CreateResponse(HttpStatusCode.OK);

            string rootPath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/user-content");
            string filePath = Path.Combine(rootPath, "book-images", fileName);

            response.Content = new StreamContent(new FileStream($"{filePath}", FileMode.Open)); // this file stream will be closed by lower layers of web api for you once the response is completed.
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

            return response;
        }


        [HttpPost]
        [ImageFilterAttribute]
        public async Task<HttpResponseMessage> PostImage()
        {
            List<string> fileAdded = new List<string>();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {
                var httpRequest = HttpContext.Current.Request;

                if (httpRequest.Files.Count == 0)
                {
                    var res = string.Format("Please Upload a image.");
                    dict.Add("error", res);
                    return Request.CreateResponse(HttpStatusCode.NotFound, dict);
                }

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();

                        string myImageFileName = Guid.NewGuid() + extension;
                        //  where you want to attach your imageurl

                        //if needed write the code to update the table
                        BookImageDTO bookImageDTO = new BookImageDTO()
                        {
                            BookId = 1,
                            DateCreated = DateTime.Now,
                            Caption = "sorry my mistake",
                            FileSize = postedFile.ContentLength,
                            IsDefault = true,
                            SortOrder = 1,
                            ImagePath = $"{_storageService.GetFileUrl(myImageFileName)}"
                        };
                        await _bookImageManagerService.Add(bookImageDTO);
                        await _bookImageManagerService.SaveToDb();

                        await _storageService.SaveFileAsync(postedFile.InputStream, myImageFileName);
                        fileAdded.Add(myImageFileName);
                    }
                }
                var message1 = string.Format("Image Updated Successfully.");
                return Request.CreateErrorResponse(HttpStatusCode.Created, message1);
            }
            catch (Exception ex)
            {
                if (fileAdded.Count != 0)
                {
                    foreach (string filename in fileAdded)
                    {
                        await _storageService.DeleteFileAsync(filename);
                    }
                }

                dict.Add("error", ex.Message);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }
    }
}
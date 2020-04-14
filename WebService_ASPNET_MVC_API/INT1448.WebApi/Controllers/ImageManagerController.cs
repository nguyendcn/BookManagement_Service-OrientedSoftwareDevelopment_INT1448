using INT1448.Application.Storage;
using INT1448.Shared.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace INT1448.WebApi.Controllers
{
    [RoutePrefix("api/imagemanagers")]
    public class ImageManagerController : ApiController
    {
        private IStorageService _storageService;

        public ImageManagerController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpPost]
        [ImageFilterAttribute]
        public HttpResponseMessage PostImage()
        {
            List<string> fileAdded = new List<string>();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {
                var httpRequest = HttpContext.Current.Request;

                if(httpRequest.Files.Count == 0)
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

                        _storageService.SaveFileAsync(postedFile.InputStream, myImageFileName);
                        fileAdded.Add(myImageFileName);
                    }
                }
                var message1 = string.Format("Image Updated Successfully.");
                return Request.CreateErrorResponse(HttpStatusCode.Created, message1);
            }
            catch (Exception ex)
            {
                if(fileAdded.Count != 0)
                {
                    foreach(string filename in fileAdded)
                    {
                        _storageService.DeleteFileAsync(filename);
                    }
                }

                dict.Add("error", ex.Message);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
        }
    }
}

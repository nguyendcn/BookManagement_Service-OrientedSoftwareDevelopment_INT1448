using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace INT1448.Shared.Filters
{
    public class ImageFilterAttribute : ActionFilterAttribute
    {
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(async () =>
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                if (actionContext.Request.Content.IsMimeMultipartContent())
                {
                    var provider = actionContext.Request.Content.ReadAsMultipartAsync(cancellationToken).Result;

                    foreach (var content in provider.Contents)
                    {
                        //Here logic to check extension, magic number and length.
                        //If any error occurred then throw exception with HttpStatusCode
                        var fileName = content.Headers.ContentDisposition == null ? string.Empty : content.Headers.ContentDisposition.FileName;
                        var fileInBytes = content.ReadAsByteArrayAsync().Result;

                        string ext = String.Empty;
                        if (fileName.Contains("\""))
                        {
                            ext = fileName.Replace("\"", "");
                        }

                        ext = ext.Substring(fileName.LastIndexOf('.'));
                        string extension = ext.ToLower();
                        var validExtensions = new List<string>() { "jpg", "gif", "png" };
                        if (!validExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                        {
                            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                            dict.Add("error", message);
                            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }

                        if (fileInBytes != null && fileInBytes.Any() && fileInBytes.Length >= 3000000)
                        {
                            var message = string.Format("Please Upload a file upto 3 mb.");

                            dict.Add("error", message);
                            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                    }
                }
            }, cancellationToken);
        }
    }
}
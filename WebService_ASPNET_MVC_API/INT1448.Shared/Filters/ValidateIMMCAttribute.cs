using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace INT1448.Shared.Filters
{
    public class ValidateIMMCAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.Request.Content.IsMimeMultipartContent())
            {
                actionContext.Response = actionContext.Request.
                    CreateResponse(HttpStatusCode.UnsupportedMediaType, "error: Unsupported this media type.");
            }
        }
    }
}
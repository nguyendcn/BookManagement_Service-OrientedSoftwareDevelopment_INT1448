using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace INT1448.Shared.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;

            if (!modelState.IsValid)
                actionContext.Response = actionContext.Request
                     .CreateErrorResponse(HttpStatusCode.BadRequest, modelState);
            //else
            //{
            //    var errors = new List<string>();
            //    foreach (var state in modelState)
            //    {
            //        foreach (var error in state.Value.Errors)
            //        {
            //            errors.Add(error.ErrorMessage);
            //        }
            //    }

            //    var response = new { errors = errors };

            //    actionContext.Response = actionContext.Request
            //    .CreateResponse(HttpStatusCode.BadRequest, response, JsonMediaTypeFormatter.DefaultMediaType);
            //}
        }
    }
}

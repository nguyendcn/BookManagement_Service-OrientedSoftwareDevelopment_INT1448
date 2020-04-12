using INT1448.Shared.CommonType;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;


namespace INT1448.Shared.Filters
{
    public class IDFilterAttribute: ActionFilterAttribute
    {

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var parameters = actionContext.ActionDescriptor.GetParameters();

            foreach (var parameter in parameters)
            {
                var argument = actionContext.ActionArguments[parameter.ParameterName];

                if(argument == null)
                {
                    ParameterError error = new ParameterError("false", "ID is not valid required.");
                    var response = error;
                    actionContext.Response = actionContext.Response = actionContext.Request
                        .CreateResponse(HttpStatusCode.BadRequest, response, JsonMediaTypeFormatter.DefaultMediaType);
                    break;
                }

                if (parameter.ParameterName.Equals("id"))
                {
                    Type t = argument.GetType();
                    if (t == typeof(int)) 
                    {
                        if (Convert.ToInt32(argument) < 0)
                        {
                            ParameterError error = new ParameterError("false", "Not match with system format.");
                            var response = error;
                            actionContext.Response = actionContext.Response = actionContext.Request
                                .CreateResponse(HttpStatusCode.BadRequest, response, JsonMediaTypeFormatter.DefaultMediaType);
                        }
                    }
                    else
                    {
                        ParameterError error = new ParameterError("false", "ID is not valid for Int32.");
                        var response = error;
                        actionContext.Response = actionContext.Response = actionContext.Request
                            .CreateResponse(HttpStatusCode.BadRequest, response, JsonMediaTypeFormatter.DefaultMediaType);
                    }
                }
            }
        }
    }
}

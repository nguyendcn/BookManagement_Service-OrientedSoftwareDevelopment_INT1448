using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                if (parameter.ParameterName.Equals("id"))
                {
                    Debug.WriteLine(parameter.DefaultValue);
                }
            }

            base.OnActionExecuting(actionContext);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPI
{
    public class InterceptionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var x = "This is my custom line of code I need executed before any of the controller actions";
            base.OnActionExecuting(actionContext);
        }
    }
}
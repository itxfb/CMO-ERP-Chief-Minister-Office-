using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Mvc.Formatters;
using pk.gov.pitb.cmo.directive.web.Core.Middleware;

namespace pk.gov.pitb.cmo.directive.web.Core.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthenticationFilter : Attribute, IActionFilter
    {
        private string[] _permissioStrings;
        public AuthenticationFilter(params string[] permission)
        {
            this._permissioStrings = permission;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (_permissioStrings[0] == "1sami")
            {

                ObjectResult? r = context.Result as ObjectResult;

                var unauthorizedObject = new UnauthorizedObjectResult("You're not authorized for this action");




                unauthorizedObject.ContentTypes = new MediaTypeCollection();
                unauthorizedObject.ContentTypes.Add("application/json");


                context.Result = unauthorizedObject;
                
                
              
            }
                
            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }
    }
}

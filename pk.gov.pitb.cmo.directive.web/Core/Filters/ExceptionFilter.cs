using System.Net;
using Azure;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using pk.gov.pitb.cmo.directive.domain.Exceptions;
using pk.gov.pitb.cmo.directive.web.Core.Middleware;

namespace pk.gov.pitb.cmo.directive.web.Core.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {

            HttpStatusCode httpStatus = HttpStatusCode.InternalServerError;
            if (context.Exception is AuthenticationException)
            {
                httpStatus = HttpStatusCode.Unauthorized;
            }

           



            string url = context.HttpContext.Request.GetDisplayUrl();
            var errorResult = new ObjectResult(

                    ResponseWrapManager.ResponseWrapper(null, url, httpStatus, context.Exception)
                   );


            errorResult.StatusCode = (int)httpStatus;
            errorResult.ContentTypes = new MediaTypeCollection();
            errorResult.ContentTypes.Add("application/json");
            context.Result = errorResult;



            base.OnException(context);
        }

    }
}

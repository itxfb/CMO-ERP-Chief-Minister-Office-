

using System.Net;
using System.Text;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http.Extensions;
using pk.gov.pitb.cmo.directive.web.Core.Middleware;

namespace pk.gov.pitb.cmo.directive.web.Core.Filters
{
    public class ResponseFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            ObjectResult? r = context.Result as ObjectResult;
            string url = context.HttpContext.Request.GetDisplayUrl();
            HttpStatusCode statusCode = HttpStatusCode.OK;

            if (r != null && r.StatusCode.HasValue)
            {
                statusCode = (HttpStatusCode)r.StatusCode;
            }

            var okResult = new ObjectResult(

                    ResponseWrapManager.ResponseWrapper(r?.Value, url, statusCode));



            okResult.ContentTypes = new MediaTypeCollection();
            var contentType = context?.Result?.GetType()?.GetProperty("ContentType")?.GetValue(context.Result) ?? "";
            if (!string.IsNullOrEmpty(contentType.ToString()))
            {
                okResult.ContentTypes.Add(contentType.ToString());
                context.Result = context.Result;
            }
            else
            {
                
                okResult.ContentTypes.Add("application/json");
                context.Result = okResult;
                
            }

            okResult.StatusCode = (int)statusCode;
            base.OnResultExecuting(context);
            //context.Result = okResult;


        }
    }


}

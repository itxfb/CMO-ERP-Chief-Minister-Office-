using System.Net;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using pk.gov.pitb.cmo.directive.domain.Exceptions;

namespace pk.gov.pitb.cmo.directive.web.Core.Middleware
{
    public class ExceptionMiddleware
    {
        /// <summary>
        /// Request Delegate field to invoke HTTP Context
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// The Response Wrapper Middleware Constructor
        /// </summary>
        /// <param name="next">The Request Delegate</param>
        public ExceptionMiddleware(RequestDelegate next) => _next = next;

        /// <summary>
        /// Invoke Method for the HttpContext
        /// </summary>
        /// <param name="context">The HTTP Context</param>
        /// <returns>Response</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            object response = null;
            try
            {

                await _next(context);


            }
            catch (Exception e)
            {
                HttpStatusCode httpStatus = HttpStatusCode.InternalServerError;
                if (e is AuthenticationException)
                {
                    httpStatus = HttpStatusCode.Unauthorized;
                }

                string url = context.Request.GetDisplayUrl();

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)httpStatus;
                response = ResponseWrapManager.ResponseWrapper(null, url, httpStatus, e);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));


            }




        }
    }
}

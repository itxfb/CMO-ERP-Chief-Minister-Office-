using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Net;
using Newtonsoft.Json;
using Azure;

namespace pk.gov.pitb.cmo.directive.web.Core.Middleware
{
    public static class ConfigureCustomExceptionMiddleware
    {

        public static void ConfigureCustomExceptionM(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }

        public static void ConfigureCustomExceptionHandler(this IApplicationBuilder app)
        {
            //  app.UseMiddleware<ExceptionMiddleware>();

            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var r =  new Exception("Sami");

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(contextFeature.Error));
                    }
                });
            });

        }

        

    }
}

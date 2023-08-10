using System.Net;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;

namespace pk.gov.pitb.cmo.directive.web.Core.Middleware
{

    /// <summary>
    /// Response Wrapper Middleware to Request Delegate and handles Request/Response Customizations.
    /// </summary>
    public class ResponseWrapperMiddleware
    {
        /// <summary>
        /// Request Delegate field to invoke HTTP Context
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// The Response Wrapper Middleware Constructor
        /// </summary>
        /// <param name="next">The Request Delegate</param>
        public ResponseWrapperMiddleware(RequestDelegate next) => _next = next;

        /// <summary>
        /// Invoke Method for the HttpContext
        /// </summary>
        /// <param name="context">The HTTP Context</param>
        /// <returns>Response</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            object response = null;

            // Storing Context Body Response
            var currentBody = context.Response.Body;

            // Using MemoryStream to hold Controller Response
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            // Passing call to Controller
            await _next(context);

            // Resetting Context Body Response
            context.Response.Body = currentBody;

            // Setting Memory Stream Position to Beginning
            memoryStream.Seek(0, SeekOrigin.Begin);

            // Read Memory Stream data to the end
            var readToEnd = new StreamReader(memoryStream).ReadToEnd();

            // Deserializing Controller Response to an object


            var result = await Task.Run(() => JsonConvert.DeserializeObject(readToEnd));
            if (result is SampleResponse value)
            {
                if (!value.Status)
                {
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(value));

                }
            }
            else
            {
                // Invoking Customizations Method to handle Custom Formatted Response
              //  response = ResponseWrapManager.ResponseWrapper(result, context);
              //  await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }





        }
    }


    public static class ResponseWrapManager
    {
        /// <summary>
        /// The Response Wrapper method handles customizations and generate Formatted Response.
        /// </summary>
        /// <param name="result">The Result</param>
        /// <param name="context">The HTTP Context</param>
        /// <param name="exception">The Exception</param>
        /// <returns>Sample Response Object</returns>
        public static SampleResponse ResponseWrapper(object? result, string url, HttpStatusCode statusCode, Exception? exception = null)
        {

            var data = result;
            var error = exception != null ? exception.Message : null;
            var status = statusCode == HttpStatusCode.OK;

            // NOTE: Add any further customizations if needed here

            return new SampleResponse(url, data, error, status, statusCode);
        }
    }


    [Serializable]
    public class SampleResponse
    {
        /// <summary>
        /// The Sample Response Constructor
        /// </summary>
        /// <param name="requestUrl">The Request Url</param>
        /// <param name="data">The Data</param>
        /// <param name="error">The Error</param>
        /// <param name="status">The Status</param>
        /// <param name="httpStatusCode">The Http Status Code</param>
        public SampleResponse(
            string requestUrl,
            object? data,
            string? error,
            bool status = false,
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
        {
            RequestUrl = requestUrl;
            Data = data;
            Error = error;
            Status = status;
            StatusCode = httpStatusCode;
        }

        /// <summary>
        /// The Request Url
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// The Response Data
        /// </summary>
        public object? Data { get; set; }

        /// <summary>
        /// The Response Error
        /// </summary>
        public string? Error { get; set; }

        /// <summary>
        /// The Response Message
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// The Response Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// The Response Http Status Code
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
    }




}

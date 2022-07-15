using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Skinet.Errors;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Skinet.ExceptionMiddleWare
{
    public class ExceptionMiddle
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddle> _logger;
        private readonly IHostEnvironment _hostEnvironment;

        public ExceptionMiddle(
            RequestDelegate next, 
            ILogger<ExceptionMiddle> logger,
            IHostEnvironment hostEnvironment)
        {
            _next = next;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
              await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _hostEnvironment.IsDevelopment() ?
                    new APIException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new APIException((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
                var json =  JsonSerializer.Serialize(response, options);

                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}

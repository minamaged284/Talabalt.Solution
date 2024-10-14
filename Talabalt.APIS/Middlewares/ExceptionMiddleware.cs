using Azure.Core.Serialization;
using System.Net;
using System.Text.Json;
using Talabalt.APIS.Errors;

namespace Talabalt.APIS.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
        {
           _next = next;
           _logger = logger;
           _env = env;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = _env.IsDevelopment()? new ServerErrorResponse((int)HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace):
                                new ServerErrorResponse((int)HttpStatusCode.InternalServerError, ex.Message);

                var serializerOptions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var jsonResponse = JsonSerializer.Serialize(response, serializerOptions);
                await context.Response.WriteAsync(jsonResponse);
            }
        }


    }
}

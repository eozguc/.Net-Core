using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using BookStore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BookStore.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly ILoggerService _loggerService;
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                var message = "[Request]  HTTP" + context.Request.Method + "-" + context.Request.Path;
                _loggerService.Write(message);

                await _next(context);
                watch.Stop();
                message = "[Response] HTTP " + context.Request.Method + "-" + context.Request.Path + " responded " +
                          context.Response.StatusCode
                          + " in " + watch.Elapsed.TotalMilliseconds + " ms";
                _loggerService.Write(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);
            }
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            var message = "[Error]    HTTP " + context.Request.Method + " - " + context.Response.StatusCode +
                          "Error Message"
                          + ex.Message + " in " + watch.Elapsed.TotalMilliseconds + " ms";
            _loggerService.Write(message);

            var result = JsonConvert.SerializeObject(new {error = ex.Message}, Formatting.None);
            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
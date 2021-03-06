using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MysteryShop.Domain.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MysteryShop.Filters
{
    public class ExceptionHandlerMiddleware
    {
        public readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ExceptionHandlerMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _configuration);
            }
        }

        public static Task HandleExceptionAsync(HttpContext context, Exception exception, IConfiguration configuration)
        {
            var statusCode = HttpStatusCode.BadRequest;
            var exceptionType = exception.GetType();
            var exceptionMessage = exception.Message;
            var language = context.Request.Headers["Accept-Language"].ToString().ToLower();
            if (language != "pl" && language != "en")
                language = "en";
            switch (exception)
            {
                case Exception e when exceptionType == typeof(UnauthorizedAccessException):
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                case NewException e when exceptionType == typeof(NewException):
                    statusCode = HttpStatusCode.BadRequest;
                    var key = e.Message + "-" + language;
                    break;
                case Exception e when exceptionType == typeof(Exception):
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            var response = new { message = exceptionMessage };
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(payload);
        }
    }
}

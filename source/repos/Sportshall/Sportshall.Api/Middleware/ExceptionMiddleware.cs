using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Sportshall.Api.Helper;
using System;
using System.Net;
using System.Text.Json;

namespace Sportshall.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next { get; }
        private readonly IHostEnvironment _env;
        private readonly IMemoryCache _memoryCache;

        private readonly TimeSpan _rateLimitWindow = TimeSpan.FromSeconds(30);
        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment environment, IMemoryCache memoryCache)
        {
            _next = next;
            _env = environment;
            _memoryCache = memoryCache;
        }

        public async  Task InvokeAsync(HttpContext context)
        {
            try
            {
                ApplySecurityHeaders(context);
                if (!IsRequestAllowed(context))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    context.Response.ContentType = "application/json";

                    var response = new ApiException((int)HttpStatusCode.TooManyRequests, "Rate limit exceeded. Try again later.");

                   await context.Response.WriteAsJsonAsync(response);
                    return;
                }
                await _next(context);

            }
            catch (Exception ex)
            {
                
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = _env.IsDevelopment()?
                    new ApiException((int) HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace)
                    : new ApiException((int)HttpStatusCode.InternalServerError, ex.Message);

                var json= JsonSerializer.Serialize(response);

               await context.Response.WriteAsync(json);

                throw;
            }

        }

        private bool IsRequestAllowed(HttpContext context)
        {
          var ip =context.Connection.RemoteIpAddress.ToString();
            var cachkey= $"Rate:{ip}";
            var datenow = DateTime.Now;

            var (timesTamp, count) = _memoryCache.GetOrCreate(cachkey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _rateLimitWindow;
                return (timesTamp:datenow,count: 0);
            }); 

            if(datenow - timesTamp < _rateLimitWindow)
            {
                if (count >= 8)
                {
                    return false;
                }
                _memoryCache.Set(cachkey, (timesTamp, count + 1), _rateLimitWindow);
            }
            else
            {
                _memoryCache.Set(cachkey, (timesTamp, count ), _rateLimitWindow);
            }

            return true;
        }

        private void ApplySecurityHeaders(HttpContext context)
        {
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
            context.Response.Headers.Add("X-Frame-Options", "DENY");
            //context.Response.Headers.Add("Referrer-Policy", "no-referrer");
            //context.Response.Headers.Add("Permissions-Policy", "geolocation=(self), microphone=()");
        }

    }
}

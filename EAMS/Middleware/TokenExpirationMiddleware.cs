using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace EAMS.Middleware
{
    public class TokenExpirationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenExpirationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Check if the user is authenticated
            if (context.User.Identity.IsAuthenticated)
            {
                // Check for the existence of the "exp" claim
                var expirationClaim = context.User.FindFirst("exp");

                if (expirationClaim != null && long.TryParse(expirationClaim.Value, out long expirationTimestamp))
                {
                    // Convert the expiration timestamp to DateTime 
                   // var expirationTime = DateTimeOffset.FromUnixTimeSeconds(expirationTimestamp).UtcDateTime;
                    
                    DateTimeOffset expirationTime = DateTimeOffset.FromUnixTimeSeconds(expirationTimestamp);
                    TimeSpan istOffset = TimeSpan.FromHours(5) + TimeSpan.FromMinutes(30);
                    DateTime expirationIST = expirationTime.UtcDateTime + istOffset;
                     


                    DateTime dateTime = DateTime.Now;
                    DateTime utcDateTime = DateTime.SpecifyKind(dateTime.ToUniversalTime(), DateTimeKind.Utc);
                    TimeZoneInfo istTimeZone = TimeZoneInfo.CreateCustomTimeZone("IST", istOffset, "IST", "IST");
                    DateTime hiINDateTimeNow = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, istTimeZone);

                    // Check if the token is expired
                    if (expirationIST < hiINDateTimeNow)
                    {
                        // Token is expired, you can handle this as needed
                        context.Response.StatusCode = 401; // Unauthorized
                        await context.Response.WriteAsync("Token has expired.");
                        return;
                    }
                }
            }

            // Continue to the next middleware in the pipeline
            await _next(context);
        }
    }
  

    public static class TokenExpirationMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenExpirationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenExpirationMiddleware>();
        }
    }
}

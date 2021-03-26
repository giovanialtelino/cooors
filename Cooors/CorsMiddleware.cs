using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;


namespace Cooors.Middleware
{
    public class DefaultFrontEntCors
    {
        private readonly RequestDelegate _next;

        public DefaultFrontEntCors(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", "https://front-cooors.giovanialtelino.com");
            context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            context.Response.Headers.Add("Access-Control-Expose-Headers", "Set-Cookie");

            var opts = new CookieOptions
            {
                Domain = "https://front-cooors.giovanialtelino.com",
                HttpOnly = true,
                MaxAge = TimeSpan.FromTicks(DateTime.Now.AddHours(1).Ticks),
                SameSite = SameSiteMode.Lax,
                Secure = true

            };

            context.Response.Cookies.Append("fake-jwt", "this-is-a-automated-jwt-cookie", opts);

            await _next(context);
        }
    }
}
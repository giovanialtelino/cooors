using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

namespace Cooors.Middleware
{
    public class FakeCookieReader
    {
        private readonly RequestDelegate _next;

        public FakeCookieReader(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.Headers.TryGetValue("fake-jwt", out var jwt);

            if(!string.IsNullOrEmpty(jwt)) Console.WriteLine(jwt);

            await _next(context);
        }
    }
}
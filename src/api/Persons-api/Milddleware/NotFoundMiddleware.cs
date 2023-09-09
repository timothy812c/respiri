using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Persons_api.Milddleware
{
    public class NotFoundMiddleware
    {
        private readonly RequestDelegate _next;

        public NotFoundMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == 404)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 404;

                var notFoundResponse = new ErrorResponse
                {
                    Message = "The requested resource was not found. Please contact Timothy if you think there was a mistake."
                };

                await context.Response.WriteAsync(notFoundResponse.ToString());
            }
        }
    }
}

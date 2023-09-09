using Persons_api.Exceptions;
using System.Net;


namespace Persons_api.Milddleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "An error occurred while processing your request.";

            if (ex.InnerException is NotFoundException notFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                message = notFoundException.Message;
            }
            else if (ex.InnerException is BadRequestException badRequestException)
            {
                statusCode = HttpStatusCode.BadRequest;
                message = badRequestException.Message;
            }
            else if (ex.InnerException is UnauthorizedException unauthorizedException)
            {
                statusCode = HttpStatusCode.Unauthorized;
                message = unauthorizedException.Message;
            }
            else if (ex.InnerException is ForbiddenException forbiddenException)
            {
                statusCode = HttpStatusCode.Forbidden;
                message = forbiddenException.Message;
            }

            var response = new ErrorResponse { Message = message };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(response.ToString());
        }
    }
}

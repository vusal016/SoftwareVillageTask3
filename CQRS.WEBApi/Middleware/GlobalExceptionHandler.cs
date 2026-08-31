using StreamVibe.Domain.Exceptions;

namespace StreamVibe.WEBApi.Middleware
{
    public sealed class GlobalExceptionHandler(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                int statusCode = ex switch
                {
                    UnauthorizedAccessException => 401,
                    ForbiddenException => 403,
                    ArgumentException => 400,
                    KeyNotFoundException => 404,
                    InvalidOperationException => 409,
                    _ => 500
                };

                var response = Response<string>.Fail(
                    ex.Message,
                    statusCode);

                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
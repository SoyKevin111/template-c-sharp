using System.Security.Authentication;
using templatebase.src.Common;

namespace templatebase.src.Core
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppException ex)
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = ex.StatusCode;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new { error = ex.Message });
                }
            }
            catch (AuthenticationException)
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new { error = "Credenciales inválidas" });
                }
            }
            catch (Exception)
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                }
            }
        }
    }

}
using System.Security.Authentication;
using templatebase.src.Common;

namespace templatebase.src.Core
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppException ex) when (!context.Response.HasStarted)
            {
                await WriteJsonResponse(context, ex.StatusCode, ex.Message);
            }
            catch (AuthenticationException) when (!context.Response.HasStarted)
            {
                await WriteJsonResponse(context, 401, "Credenciales inválidas");
            }
            catch (Exception) when (!context.Response.HasStarted)
            {
                await WriteJsonResponse(context, 500, "Error interno en el servidor.");
            }
        }

        private static Task WriteJsonResponse(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsJsonAsync(new { error = message });
        }
    }
}

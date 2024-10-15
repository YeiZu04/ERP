using System.ComponentModel.DataAnnotations;

namespace ERP_API.Services.Tools
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var statusCode = exception switch
            {
                ArgumentNullException => StatusCodes.Status400BadRequest,          // Bad Request: argumento nulo
                ArgumentException => StatusCodes.Status400BadRequest,              // Bad Request: argumento inválido
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,  // Unauthorized: no autenticado
                InvalidOperationException => StatusCodes.Status403Forbidden,       // Forbidden: sin permisos
                KeyNotFoundException => StatusCodes.Status404NotFound,             // Not Found: recurso no encontrado
                ValidationException => StatusCodes.Status422UnprocessableEntity,   // Unprocessable Entity: validación fallida
                TimeoutException => StatusCodes.Status504GatewayTimeout,           // Gateway Timeout: tiempo de espera
                _ => StatusCodes.Status500InternalServerError                      // General Server Error: error interno
            };

            response.StatusCode = statusCode;

            return response.WriteAsJsonAsync(new
            {
                Success = false,
                StatusCode = statusCode,
                ErrorMessage = exception.Message
            });
        }
    }
}

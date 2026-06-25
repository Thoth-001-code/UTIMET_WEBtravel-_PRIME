using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QuanLyDuLich.Middlewares
{
    /// <summary>
    /// Middleware bắt lỗi toàn cục, trả về JSON thân thiện cho client
    /// </summary>
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // Mặc định là 500 Internal Server Error
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = exception.Message; // Always show actual message

            // Get inner exception message if exists
            var innerException = exception.InnerException;
            while (innerException != null)
            {
                message += " | Inner: " + innerException.Message;
                innerException = innerException.InnerException;
            }

            // Có thể tùy chỉnh status code và message dựa trên loại exception
            if (exception is UnauthorizedAccessException)
            {
                statusCode = (int)HttpStatusCode.Unauthorized;
            }
            else if (exception is InvalidOperationException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (exception is KeyNotFoundException)
            {
                statusCode = (int)HttpStatusCode.NotFound;
            }
            // Bạn có thể thêm các loại exception khác tùy ý

            context.Response.StatusCode = statusCode;

            var response = new
            {
                error = message,
                // Chỉ hiển thị stack trace trong môi trường Development
                stackTrace = context.RequestServices.GetService<IWebHostEnvironment>()?.IsDevelopment() == true
                    ? exception.StackTrace
                    : null
            };

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return context.Response.WriteAsync(json);
        }
    }
}
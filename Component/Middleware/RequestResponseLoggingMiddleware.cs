using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TaiLieuWebsiteBackend.Component.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _basePath;

        public RequestResponseLoggingMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _basePath = configuration["FileStorage:BasePath"];
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log request
            var request = await FormatRequest(context.Request);
            await LogToFileAsync("Request", request);

            // Copy original response body stream
            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                // Log response
                var response = await FormatResponse(context.Response);
                await LogToFileAsync("Response", response);

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();
            var body = request.Body;

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"Response {response.StatusCode}: {text}";
        }

        private async Task LogToFileAsync(string type, string log)
        {
            var fileName = $"{type}_{DateTime.Now:yyyyMMddHHmmssfff}.txt";
            var filePath = Path.Combine(_basePath, fileName);

            await File.WriteAllTextAsync(filePath, log);
        }
    }
}

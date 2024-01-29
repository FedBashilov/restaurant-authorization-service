// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Web.Facade.Middlewares
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class HttpRequestBodyMiddleware
    {
        private readonly ILogger<HttpRequestBodyMiddleware> logger;
        private readonly RequestDelegate next;

        public HttpRequestBodyMiddleware(
            RequestDelegate next,
            ILogger<HttpRequestBodyMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();
            var reader = new StreamReader(context.Request.Body);

            string body = await reader.ReadToEndAsync();
            this.logger.LogInformation(
                      $"Request {context.Request?.Method}: {context.Request?.Path.Value}\n{body}");

            context.Request!.Body.Position = 0L;

            await this.next(context);
        }
    }
}
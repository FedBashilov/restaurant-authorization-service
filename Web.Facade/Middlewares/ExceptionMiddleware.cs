// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Web.Facade.Middlewares
{
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Authorization.Service.Exceptions;
    using CloudStorage.Service.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using Users.Service.Exceptions;
    using Web.Facade.Controllers;
    using Web.Facade.Models.Responses;

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IStringLocalizer<AuthController> localizer;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            IStringLocalizer<AuthController> localizer,
            ILogger<ExceptionMiddleware> logger)
        {
            this.next = next;
            this.localizer = localizer;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception exception)
            {
                this.logger.LogError(
                    exception,
                    $"Request {context.Request?.Method}: {context.Request?.Path.Value} failed");
                var response = context.Response;

                var (status, message) = this.GetResponse(exception);
                response.StatusCode = (int)status;
                await response.WriteAsJsonAsync(new ErrorResponse(message));
            }
        }

        private (HttpStatusCode code, string msg) GetResponse(Exception ex)
        {
            HttpStatusCode code;
            var msg = string.Empty;
            switch (ex)
            {
                case LogInFailedException:
                    code = HttpStatusCode.BadRequest;
                    msg = this.localizer["Wrong Email or Password"].Value;
                    break;
                case RegisterFailedException:
                    var messages = ex.Message.Split(";");
                    var messageBuilder = new StringBuilder();
                    foreach (var message in messages)
                    {
                        messageBuilder.AppendLine(this.localizer[message].Value);
                    }

                    code = HttpStatusCode.BadRequest;
                    msg = messageBuilder.ToString();
                    break;
                case EmailVerificationFailedException:
                    code = HttpStatusCode.BadRequest;
                    msg = this.localizer["Invalid email token"].Value;
                    break;
                case EmailNotConfirmedException:
                    code = HttpStatusCode.BadRequest;
                    msg = this.localizer["Email not confirmed"].Value;
                    break;
                case RefreshTokensFailedException:
                    code = HttpStatusCode.BadRequest;
                    msg = this.localizer["Invalid access or refresh tokens"].Value;
                    break;
                case UserNotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
                case GoogleVerificationFailedException:
                    code = HttpStatusCode.InternalServerError;
                    msg = this.localizer["Failed to verify Google OAuth token."].Value + ex.Message;
                    break;
                case UpdateUserFailedException:
                    msg = this.localizer["Unexpected server error"].Value;
                    code = HttpStatusCode.InternalServerError;
                    break;
                default:
                    msg = this.localizer["Unexpected server error"].Value;
                    code = HttpStatusCode.InternalServerError;
                    break;
            }

            return (code, msg);
        }
    }
}
// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Identity.Server.Controllers
{
    using System.Net.Mail;
    using System.Text;
    using Identity.Server.Exceptions;
    using Identity.Server.Models;
    using Identity.Server.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;

    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IStringLocalizer<AuthController> localizer;

        private readonly ILogger<AuthController> logger;

        public AuthController(
            IAuthService authService,
            IStringLocalizer<AuthController> localizer,
            ILogger<AuthController> logger)
        {
            this.authService = authService;
            this.localizer = localizer;
            this.logger = logger;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto userDto)
        {
            if (userDto == null) { return this.BadRequest(new ErrorResponse(this.localizer["Invalid request body"].Value)); }
            if (userDto.Email == null) { return this.BadRequest(new ErrorResponse(this.localizer["Email is required"].Value)); }
            if (!MailAddress.TryCreate(userDto.Email, out _)) { return this.BadRequest(new ErrorResponse(this.localizer["Invalid Email"].Value)); }
            if (userDto.Password == null) { return this.BadRequest(new ErrorResponse(this.localizer["Password is required"].Value)); }
            if (userDto.Password.Length < 8) { return this.BadRequest(new ErrorResponse(this.localizer["Password must be at least 8 characters"].Value)); }

            try
            {
                var newUser = await this.authService.Register(userDto, "client");
                newUser.PasswordHash = null;
                this.logger.LogInformation($"The user with id = {newUser.Id} registred successfully! Sending 201 response...");
                return this.Ok(newUser);
            }
            catch (RegisterFailedException ex)
            {
                this.logger.LogWarning(ex, $"Can't register user. {ex.Message} Sending 400 response...");

                var messages = ex.Message.Split(";");
                var messageBuilder = new StringBuilder();
                foreach (var message in messages)
                {
                    messageBuilder.AppendLine(this.localizer[message].Value);
                }

                return this.BadRequest(new ErrorResponse(messageBuilder.ToString()));
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(ex, $"Can't register user. Unexpected server error. Sending 500 response...");
                return this.StatusCode(500, new ErrorResponse(this.localizer["Unexpected server error"].Value));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("register/cook")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> RegisterCook([FromBody] RegisterUserDto userDto)
        {
            if (userDto == null) { return this.BadRequest(new ErrorResponse(this.localizer["Invalid request body"].Value)); }
            if (userDto.Email == null) { return this.BadRequest(new ErrorResponse(this.localizer["Email is required"].Value)); }
            if (!MailAddress.TryCreate(userDto.Email, out _)) { return this.BadRequest(new ErrorResponse(this.localizer["Invalid Email"].Value)); }
            if (userDto.Password == null) { return this.BadRequest(new ErrorResponse(this.localizer["Password is required"].Value)); }
            if (userDto.Password.Length < 8) { return this.BadRequest(new ErrorResponse(this.localizer["Password must be at least 8 characters"].Value)); }

            try
            {
                var newUser = await this.authService.Register(userDto, "cook");
                newUser.PasswordHash = null;
                this.logger.LogInformation($"The user with id = {newUser.Id} registred successfully! Sending 201 response...");
                return this.Ok(newUser);
            }
            catch (RegisterFailedException ex)
            {
                this.logger.LogWarning(ex, $"Can't register user. {ex.Message} Sending 400 response...");

                var messages = ex.Message.Split(" ");
                var messageBuilder = new StringBuilder();
                foreach (var message in messages)
                {
                    messageBuilder.AppendLine(this.localizer[message].Value);
                }

                return this.BadRequest(new ErrorResponse(messageBuilder.ToString()));
            }
            catch (Exception ex)
            {
                this.logger.LogWarning(ex, $"Can't register user. Unexpected server error. Sending 500 response...");
                return this.StatusCode(500, new ErrorResponse(this.localizer["Unexpected server error"].Value));
            }
        }

        [HttpPost]
        [Route("refresh-token")]
        [ProducesResponseType(200, Type = typeof(AuthResponse))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            if (refreshTokenDto == null) { return this.BadRequest(new ErrorResponse(this.localizer["Invalid request body"].Value)); }
            if (refreshTokenDto.AccessToken == null) { return this.BadRequest(new ErrorResponse(this.localizer["AccessToken is required"].Value)); }
            if (refreshTokenDto.RefreshToken == null) { return this.BadRequest(new ErrorResponse(this.localizer["RefreshToken is required"].Value)); }

            try
            {
                var tokens = await this.authService.RefreshTokens(refreshTokenDto.AccessToken, refreshTokenDto.RefreshToken);
                return this.Ok(tokens);
            }
            catch (Exception ex)
            {
                if (ex is InvalidRefreshTokenException || ex is UserNotFoundException)
                {
                    this.logger.LogWarning(ex, $"Can't refresh user tokens. {ex.Message} Sending 400 response...");
                    return this.BadRequest(new ErrorResponse(this.localizer["Invalid access or refresh tokens"].Value));
                }

                this.logger.LogWarning(ex, $"Can't refresh user tokens. Unexpected server error. Sending 500 response...");
                return this.StatusCode(500, new ErrorResponse(this.localizer["Unexpected server error"].Value));
            }
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(200, Type = typeof(AuthResponse))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> LogIn([FromBody] LogInUserDto userDto)
        {
            if (userDto == null) { return this.BadRequest(new ErrorResponse(this.localizer["Invalid request body"].Value)); }
            if (userDto.Email == null) { return this.BadRequest(new ErrorResponse(this.localizer["Email is required"].Value)); }
            if (userDto.Password == null) { return this.BadRequest(new ErrorResponse(this.localizer["Password is required"].Value)); }

            try
            {
                var tokens = await this.authService.LogIn(userDto.Email, userDto.Password);
                return this.Ok(tokens);
            }
            catch (Exception ex)
            {
                if (ex is WrongPasswordException || ex is UserNotFoundException)
                {
                    this.logger.LogWarning(ex, $"Can't log in user. {ex.Message} Sending 400 response...");
                    return this.BadRequest(new ErrorResponse(this.localizer["Wrong Email or Password"].Value));
                }

                this.logger.LogWarning(ex, $"Can't log in user. Unexpected server error. Sending 500 response...");
                return this.StatusCode(500, new ErrorResponse(this.localizer["Unexpected server error"].Value));
            }
        }
    }
}

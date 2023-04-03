// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Web.Facade.Controllers
{
    using System.Net.Mail;
    using System.Text;
    using Authorization.Service;
    using Authorization.Service.Exceptions;
    using Authorization.Service.Models.DTOs;
    using Authorization.Service.Models.Responses;
    using Infrastructure.Core.Constants;
    using Infrastructure.Core.Models;
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

        [HttpPost("register")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO userDto)
        {
            if (!RegisterUserDTO.IsValidModel(userDto, out var errorMessage))
            {
                return this.BadRequest(new ErrorResponse(this.localizer[errorMessage].Value));
            }

            try
            {
                var newUser = await this.authService.Register(userDto, UserRoles.Client);
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

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("register/cook")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> RegisterCook([FromBody] RegisterUserDTO userDto)
        {
            if (!RegisterUserDTO.IsValidModel(userDto, out var errorMessage))
            {
                return this.BadRequest(new ErrorResponse(this.localizer[errorMessage].Value));
            }

            try
            {
                var newUser = await this.authService.Register(userDto, UserRoles.Cook);
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

        [HttpPost("refresh-token")]
        [ProducesResponseType(200, Type = typeof(AuthResponse))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDTO refreshTokenDto)
        {
            if (!RefreshTokenDTO.IsValidModel(refreshTokenDto, out var errorMessage))
            {
                return this.BadRequest(new ErrorResponse(this.localizer[errorMessage].Value));
            }

            try
            {
                var tokens = await this.authService.RefreshTokens(refreshTokenDto.AccessToken!, refreshTokenDto.RefreshToken!);
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

        [HttpPost("login")]
        [ProducesResponseType(200, Type = typeof(AuthResponse))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> LogIn([FromBody] LogInUserDTO loginDto)
        {
            if (!LogInUserDTO.IsValidModel(loginDto, out var errorMessage))
            {
                return this.BadRequest(new ErrorResponse(this.localizer[errorMessage].Value));
            }

            try
            {
                var tokens = await this.authService.LogIn(loginDto.Email!, loginDto.Password!);
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

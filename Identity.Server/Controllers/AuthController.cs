// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Identity.Server.Controllers
{
    using System.Net.Mail;
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
        private readonly IStringLocalizer<AuthController> stringLocalizer;

        private readonly ILogger<AuthController> logger;

        public AuthController(
            IAuthService authService,
            IStringLocalizer<AuthController> stringLocalizer,
            ILogger<AuthController> logger)
        {
            this.authService = authService;
            this.stringLocalizer = stringLocalizer;
            this.logger = logger;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto userDto)
        {
            if (userDto == null) { return this.BadRequest(new ErrorResponse("Invalid request body")); }
            if (userDto.Email == null) { return this.BadRequest(new ErrorResponse("Email cannot be null")); }
            if (userDto.Password == null) { return this.BadRequest(new ErrorResponse("Password cannot be null")); }
            if (!MailAddress.TryCreate(userDto.Email, out _)) { return this.BadRequest(new ErrorResponse("Invalid Email")); }
            
            try
            {
                var newUser = await this.authService.Register(userDto, "client");
                newUser.PasswordHash = null;
                this.logger.LogInformation($"The user with id = {newUser.Id} registred successfully! Sending 201 response...");
                return this.Ok(newUser);
            }
            catch (Exception ex)
            {
                if (ex is RegisterFailedException || ex is ArgumentNullException)
                {
                    this.logger.LogWarning(ex, $"Can't register user. {ex.Message} Sending 400 response...");
                    return this.BadRequest(new ErrorResponse($"{ex.Message}"));
                }

                this.logger.LogWarning(ex, $"Can't register user. Unexpected error. Sending 500 response...");
                return this.StatusCode(500, new ErrorResponse($"Unexpected error"));
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
            if (userDto == null) { return this.BadRequest(new ErrorResponse("Invalid request body")); }
            if (userDto.Email == null) { return this.BadRequest(new ErrorResponse("Email cannot be null")); }
            if (userDto.Password == null) { return this.BadRequest(new ErrorResponse("Password cannot be null")); }
            if (!MailAddress.TryCreate(userDto.Email, out _)) { return this.BadRequest(new ErrorResponse("Invalid Email")); }

            try
            {
                var newUser = await this.authService.Register(userDto, "cook");
                newUser.PasswordHash = null;
                this.logger.LogInformation($"The user with id = {newUser.Id} registred successfully! Sending 201 response...");
                return this.Ok(newUser);
            }
            catch (Exception ex)
            {
                if (ex is RegisterFailedException || ex is ArgumentNullException)
                {
                    this.logger.LogWarning(ex, $"Can't register user. {ex.Message} Sending 400 response...");
                    return this.BadRequest(new ErrorResponse($"{ex.Message}"));
                }

                this.logger.LogWarning(ex, $"Can't register user. Unexpected error. Sending 500 response...");
                return this.StatusCode(500, new ErrorResponse($"Unexpected error"));
            }
        }

        [HttpPost]
        [Route("refresh-token")]
        [ProducesResponseType(200, Type = typeof(AuthResponse))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            if (refreshTokenDto == null) { return this.BadRequest(new ErrorResponse("Invalid request body")); }
            if (refreshTokenDto.AccessToken == null) { return this.BadRequest(new ErrorResponse("AccessToken cannot be null")); }
            if (refreshTokenDto.RefreshToken == null) { return this.BadRequest(new ErrorResponse("RefreshToken cannot be null")); }

            try
            {
                var tokens = await this.authService.RefreshTokens(refreshTokenDto.AccessToken, refreshTokenDto.RefreshToken);
                return this.Ok(tokens);
            }
            catch (Exception ex)
            {
                if (ex is InvalidRefreshTokenException || ex is ArgumentNullException || ex is UserNotFoundException)
                {
                    this.logger.LogWarning(ex, $"Can't refresh user tokens. {ex.Message} Sending 400 response...");
                    return this.BadRequest(new ErrorResponse($"Invalid access or refresh tokens"));
                }

                this.logger.LogWarning(ex, $"Can't refresh user tokens. Unexpected error. Sending 500 response...");
                return this.StatusCode(500, new ErrorResponse($"Unexpected error"));
            }
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(200, Type = typeof(AuthResponse))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> LogIn([FromBody] LogInUserDto userDto)
        {
            if (userDto == null) { return this.BadRequest(new ErrorResponse("Invalid request body")); }
            if (userDto.Email == null) { return this.BadRequest(new ErrorResponse("Email cannot be null")); }
            if (userDto.Password == null) { return this.BadRequest(new ErrorResponse("Password cannot be null")); }

            try
            {
                var tokens = await this.authService.LogIn(userDto.Email, userDto.Password);
                return this.Ok(tokens);
            }
            catch (Exception ex)
            {
                if (ex is WrongPasswordException || ex is ArgumentNullException || ex is UserNotFoundException)
                {
                    this.logger.LogWarning(ex, $"Can't log in user. {ex.Message} Sending 400 response...");
                    return this.BadRequest(new ErrorResponse($"Wrong Email or Password"));
                }

                this.logger.LogWarning(ex, $"Can't log in user. Unexpected error. Sending 500 response...");
                return this.StatusCode(500, new ErrorResponse($"Unexpected error"));
            }
        }
    }
}

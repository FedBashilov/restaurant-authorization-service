// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Identity.Server.Controllers
{
    using Identity.Server.Exceptions;
    using Identity.Server.Models;
    using Identity.Server.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        private readonly ILogger<AuthController> logger;

        public AuthController(
            IAuthService authService,
            ILogger<AuthController> logger)
        {
            this.authService = authService;
            this.logger = logger;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto userDto)
        {
            if (userDto == null) { return this.BadRequest(new ErrorResponse("Invalid request body.")); }
            if (userDto.Email == null) { return this.BadRequest(new ErrorResponse("Email cannot be null.")); }
            if (userDto.Password == null) { return this.BadRequest(new ErrorResponse("Password cannot be null.")); }

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
                    return this.BadRequest(new ErrorResponse($"Can't register user. {ex.Message}"));
                }

                this.logger.LogWarning(ex, $"Can't register user. Unexpected error. Sending 500 response...");
                return this.StatusCode(500, new ErrorResponse($"Can't register user. Unexpected error."));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("register/cook")]
        public async Task<IActionResult> RegisterCook([FromBody] RegisterUserDto userDto)
        {
            if (userDto == null) { return this.BadRequest(new ErrorResponse("Invalid request body.")); }
            if (userDto.Email == null) { return this.BadRequest(new ErrorResponse("Email cannot be null.")); }
            if (userDto.Password == null) { return this.BadRequest(new ErrorResponse("Password cannot be null.")); }

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
                    return this.BadRequest(new ErrorResponse($"Can't register user. {ex.Message}"));
                }

                this.logger.LogWarning(ex, $"Can't register user. Unexpected error. Sending 500 response...");
                return this.StatusCode(500, new ErrorResponse($"Can't register user. Unexpected error."));
            }
        }


        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            if (refreshTokenDto == null) { return this.BadRequest(new ErrorResponse("Invalid request body.")); }
            if (refreshTokenDto.AccessToken == null) { return this.BadRequest(new ErrorResponse("AccessToken cannot be null.")); }
            if (refreshTokenDto.RefreshToken == null) { return this.BadRequest(new ErrorResponse("RefreshToken cannot be null.")); }

            try
            {
                var tokens = await this.authService.RefreshTokens(refreshTokenDto.AccessToken, refreshTokenDto.RefreshToken);
                return this.Ok(tokens);
            }
            catch (Exception ex)
            {
                if (ex is InvalidRefreshTokenException || ex is ArgumentNullException)
                {
                    this.logger.LogWarning(ex, $"Can't refresh user tokens. {ex.Message} Sending 400 response...");
                    return this.BadRequest(new ErrorResponse($"Can't refresh user tokens. {ex.Message}"));
                }

                if (ex is UserNotFoundException)
                {
                    this.logger.LogWarning(ex, $"Can't refresh user tokens. {ex.Message} Sending 404 response...");
                    return this.NotFound();
                }

                this.logger.LogWarning(ex, $"Can't refresh user tokens. Unexpected error. Sending 500 response...");
                return this.StatusCode(500, new ErrorResponse($"Can't refresh user tokens.  Unexpected error."));
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LogIn([FromBody] LogInUserDto userDto)
        {
            if (userDto == null) { return this.BadRequest(new ErrorResponse("Invalid request body.")); }
            if (userDto.Email == null) { return this.BadRequest(new ErrorResponse("Email cannot be null.")); }
            if (userDto.Password == null) { return this.BadRequest(new ErrorResponse("Password cannot be null.")); }

            try
            {
                var tokens = await this.authService.LogIn(userDto.Email, userDto.Password);
                return this.Ok(tokens);
            }
            catch (Exception ex)
            {
                if (ex is WrongPasswordException || ex is ArgumentNullException)
                {
                    this.logger.LogWarning(ex, $"Can't log in user. {ex.Message} Sending 401 response...");
                    return this.BadRequest(new ErrorResponse($"Can't log in user. {ex.Message}"));
                }

                if (ex is UserNotFoundException)
                {
                    this.logger.LogWarning(ex, $"Can't log in user. {ex.Message} Sending 404 response...");
                    return this.NotFound();
                }

                this.logger.LogWarning(ex, $"Can't log in user. Unexpected error. Sending 500 response...");
                return this.StatusCode(500, new ErrorResponse($"Can't log in user.  Unexpected error."));
            }
        }
    }
}

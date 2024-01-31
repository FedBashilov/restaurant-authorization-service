// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Web.Facade.Controllers
{
    using Authorization.Service;
    using Authorization.Service.Models.DTOs;
    using Authorization.Service.Models.Responses;
    using Infrastructure.Core.Constants;
    using Infrastructure.Core.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using Web.Facade.Models.Responses;

    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IStringLocalizer<SharedResources> localizer;

        private readonly ILogger<AuthController> logger;

        public AuthController(
            IAuthService authService,
            IStringLocalizer<SharedResources> localizer,
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

            var newUser = await this.authService.Register(userDto, UserRoles.Client);

            return this.Ok(newUser);
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

            var newUser = await this.authService.Register(userDto, UserRoles.Cook);

            return this.Ok(newUser);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("register/admin")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUserDTO userDto)
        {
            if (!RegisterUserDTO.IsValidModel(userDto, out var errorMessage))
            {
                return this.BadRequest(new ErrorResponse(this.localizer[errorMessage].Value));
            }

            var newUser = await this.authService.Register(userDto, UserRoles.Admin);

            return this.Ok(newUser);
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

            var tokens = await this.authService.RefreshTokens(refreshTokenDto.AccessToken!, refreshTokenDto.RefreshToken!);

            return this.Ok(tokens);
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

            var tokens = await this.authService.LogIn(loginDto.Email!, loginDto.Password!);

            return this.Ok(tokens);
        }

        [HttpGet("verify")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Verify(
            [FromQuery] string userId,
            [FromQuery] string emailToken)
        {
            await this.authService.VerifyMail(userId, emailToken);

            return this.Ok("You have successfully verified your account email! \nNow go back to our app and choose some delicious food ;)");
        }

        [HttpPost("verify-google")]
        [ProducesResponseType(200, Type = typeof(AuthResponse))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> VerifyGoogle(
        [FromBody] VerifyGoogleDTO verifyGoogleDto)
        {
            if (!VerifyGoogleDTO.IsValidModel(verifyGoogleDto, out var errorMessage))
            {
                return this.BadRequest(new ErrorResponse(this.localizer[errorMessage].Value));
            }

            var tokens = await this.authService.VerifyGoogle(verifyGoogleDto.Token!);

            return this.Ok(tokens);
        }
    }
}

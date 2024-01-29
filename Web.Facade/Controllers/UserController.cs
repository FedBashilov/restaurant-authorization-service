// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Web.Facade.Controllers
{
    using System.Security.Claims;
    using Infrastructure.Core.Constants;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using Users.Service;
    using Users.Service.Models.Responses;
    using Web.Facade.Models.Responses;
    using Web.Facade.Services;

    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IStringLocalizer<AuthController> localizer;

        private readonly ILogger<UserController> logger;

        public UserController(
            IUserService userService,
            IStringLocalizer<AuthController> localizer,
            ILogger<UserController> logger)
        {
            this.localizer = localizer;
            this.userService = userService;
            this.logger = logger;
        }

        [Authorize(Roles = $"{UserRoles.Client}, {UserRoles.Cook}, {UserRoles.Admin}")]
        [HttpGet("_me")]
        [ProducesResponseType(200, Type = typeof(UserResponse))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetUserInfo()
        {
            var accessToken = await this.HttpContext.GetTokenAsync("access_token");
            var userId = JwtService.GetClaimValue(accessToken, ClaimTypes.Actor);

            var userInfo = await this.userService.GetUserInfo(userId);

            return this.Ok(userInfo);
        }

        [Authorize(Roles = $"{UserRoles.Client}, {UserRoles.Cook}, {UserRoles.Admin}")]
        [HttpPut("_me")]
        [ProducesResponseType(200, Type = typeof(UserResponse))]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserInfoDTO updateUserDto)
        {
            if (!UpdateUserInfoDTO.IsValidModel(updateUserDto, out var errorMessage))
            {
                return this.BadRequest(new ErrorResponse(this.localizer[errorMessage].Value));
            }

            var accessToken = await this.HttpContext.GetTokenAsync("access_token");
            var userId = JwtService.GetClaimValue(accessToken, ClaimTypes.Actor);

            var userInfo = await this.userService.UpdateUserInfo(userId, updateUserDto);

            return this.Ok(userInfo);
        }
    }
}

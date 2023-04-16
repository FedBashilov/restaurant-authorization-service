// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Web.Facade.Controllers
{
    using System.Security.Claims;
    using Infrastructure.Core.Constants;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Users.Service;
    using Users.Service.Models.Responses;
    using Web.Facade.Models.Responses;
    using Web.Facade.Services;

    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        private readonly ILogger<UserController> logger;

        public UserController(
            IUserService userService,
            ILogger<UserController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }

        [Authorize(Roles = $"{UserRoles.Client}, {UserRoles.Cook}, {UserRoles.Admin}")]
        [HttpGet("_me")]
        [ProducesResponseType(200, Type = typeof(UserResponse))]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetUserInfo()
        {
            try
            {
                var accessToken = await this.HttpContext.GetTokenAsync("access_token");
                var userId = JwtService.GetClaimValue(accessToken, ClaimTypes.Actor);

                var userInfo = await this.userService.GetUserInfo(userId);

                return this.Ok(userInfo);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Can't get user info. {ex.Message}");
                return this.StatusCode(500, new ErrorResponse("Unexpected server error"));
            }
        }
    }
}

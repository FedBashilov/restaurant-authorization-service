// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service
{
    using Authorization.Service.Models.DTOs;
    using Authorization.Service.Models.Responses;
    using Infrastructure.Core.Constants;
    using Infrastructure.Core.Models;

    public interface IAuthService
    {
        public Task<User> Register(RegisterUserDTO userDto, string userRole);

        public Task<AuthResponse> LogIn(string email, string password);

        public Task<AuthResponse> RefreshTokens(string accessToken, string refreshToken);

        public Task VerifyMail(string userId, string token);

        public Task<AuthResponse> VerifyGoogle(string token, string userRole = UserRoles.Client);
    }
}

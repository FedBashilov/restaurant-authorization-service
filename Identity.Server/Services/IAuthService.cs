// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Identity.Server.Services
{
    using Identity.Server.Models;

    public interface IAuthService
    {
        public Task<User> Register(RegisterUserDto userDto, string userRole);

        public Task<AuthResponse> LogIn(string email, string password);

        public Task<AuthResponse> RefreshTokens(string accessToken, string refreshToken);
    }
}

// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Authorization.Service.Exceptions;
    using Authorization.Service.Models;
    using Infrastructure.Core.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly JWTSettings options;

        public AuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IOptions<JWTSettings> options)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.options = options.Value;
        }

        public async Task<AuthResponse> RefreshTokens(string accessToken, string refreshToken)
        {
            var jwtToken = new JwtSecurityToken(accessToken);
            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor")?.Value;

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var refreshTokenFromDb = await userManager.GetAuthenticationTokenAsync(user, "CustomTokenProvider", "RefreshToken");
            var isValid = await userManager.VerifyUserTokenAsync(user, "CustomTokenProvider", "RefreshToken", refreshToken);

            if (!isValid || refreshToken != refreshTokenFromDb)
            {
                throw new InvalidRefreshTokenException("Invalid refresh token.");
            }

            await userManager.RemoveAuthenticationTokenAsync(user, "CustomTokenProvider", "RefreshToken");

            var tokens = await this.GetTokens(user);
            return tokens;
        }

        public async Task<User> Register(RegisterUserDto userDto, string userRole)
        {
            var user = new User { Name = userDto.Name, Email = userDto.Email, UserName = userDto.Email };

            var result = await userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, userRole);

                await signInManager.SignInAsync(user, isPersistent: false);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                claims.Add(new Claim(ClaimTypes.Actor, user.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Email, user.Email));

                await userManager.AddClaimsAsync(user, claims);
                return user;
            }
            else
            {
                throw new RegisterFailedException(string.Join(";", result.Errors.ToList().Select(e => e.Description)) ?? string.Empty);
            }
        }

        public async Task<AuthResponse> LogIn(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var result = await signInManager.PasswordSignInAsync(user, password, false, false);

            if (!result.Succeeded)
            {
                throw new WrongPasswordException("Wrong password.");
            }

            await userManager.RemoveAuthenticationTokenAsync(user, "CustomTokenProvider", "RefreshToken");

            var tokens = await this.GetTokens(user);
            return tokens;
        }

        private async Task<AuthResponse> GetTokens(User user)
        {
            var newRefreshToken = await userManager.GenerateUserTokenAsync(user, "CustomTokenProvider", "RefreshToken");
            await userManager.SetAuthenticationTokenAsync(user, "CustomTokenProvider", "RefreshToken", newRefreshToken);

            var claims = await userManager.GetClaimsAsync(user);
            var expiresIn = DateTime.UtcNow.AddDays(1);
            var token = GetAccessToken(user, claims, expiresIn);

            return new AuthResponse
            {
                AccessToken = token,
                RefreshToken = newRefreshToken,
                ExpiresIn = ((DateTimeOffset)expiresIn).ToUnixTimeSeconds(),
                TokenType = "bearer",
            };
        }

        private string GetAccessToken(User user, IEnumerable<Claim> principal, DateTime tokenExpires)
        {
            var claims = principal.ToList();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            var singingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey ?? string.Empty));

            var jwt = new JwtSecurityToken(
                    issuer: options.Issuer,
                    audience: options.Audience,
                    claims: claims,
                    expires: tokenExpires,
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(singingKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}

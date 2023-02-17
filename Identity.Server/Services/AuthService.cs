// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Identity.Server.Services
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Identity.Server.Exceptions;
    using Identity.Server.Models;
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

            var user = await this.userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var refreshTokenFromDb = await this.userManager.GetAuthenticationTokenAsync(user, "CustomTokenProvider", "RefreshToken");
            var isValid = await this.userManager.VerifyUserTokenAsync(user, "CustomTokenProvider", "RefreshToken", refreshToken);

            if (!isValid || refreshToken != refreshTokenFromDb)
            {
                throw new InvalidRefreshTokenException("Invalid refresh token.");
            }

            await this.userManager.RemoveAuthenticationTokenAsync(user, "CustomTokenProvider", "RefreshToken");

            var tokens = await this.GetTokens(user);
            return tokens;
        }

        public async Task<User> Register(RegisterUserDto userDto, string userRole)
        {
            var user = new User { Name = userDto.Name, Email = userDto.Email, UserName = userDto.Email };

            var result = await this.userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(user, userRole);

                await this.signInManager.SignInAsync(user, isPersistent: false);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                claims.Add(new Claim(ClaimTypes.Actor, user.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Email, user.Email));

                await this.userManager.AddClaimsAsync(user, claims);
                return user;
            }
            else
            {
                throw new RegisterFailedException(string.Join(";", result.Errors.ToList().Select(e => e.Description)) ?? string.Empty);
            }
        }

        public async Task<AuthResponse> LogIn(string email, string password)
        {
            var user = await this.userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var result = await this.signInManager.PasswordSignInAsync(user, password, false, false);

            if (!result.Succeeded)
            {
                throw new WrongPasswordException("Wrong password.");
            }

            await this.userManager.RemoveAuthenticationTokenAsync(user, "CustomTokenProvider", "RefreshToken");

            var tokens = await this.GetTokens(user);
            return tokens;
        }

        private async Task<AuthResponse> GetTokens(User user)
        {
            var newRefreshToken = await this.userManager.GenerateUserTokenAsync(user, "CustomTokenProvider", "RefreshToken");
            await this.userManager.SetAuthenticationTokenAsync(user, "CustomTokenProvider", "RefreshToken", newRefreshToken);

            var claims = await this.userManager.GetClaimsAsync(user);
            var expiresIn = DateTime.UtcNow.AddDays(1);
            var token = this.GetAccessToken(user, claims, expiresIn);

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

            var singingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.options.SecretKey ?? string.Empty));

            var jwt = new JwtSecurityToken(
                    issuer: this.options.Issuer,
                    audience: this.options.Audience,
                    claims: claims,
                    expires: tokenExpires,
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(singingKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}

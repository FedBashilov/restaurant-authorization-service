// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Web;
    using Authorization.Service.Exceptions;
    using Authorization.Service.Models;
    using Authorization.Service.Models.DTOs;
    using Authorization.Service.Models.Responses;
    using Google.Apis.Auth;
    using Infrastructure.Core.Constants;
    using Infrastructure.Core.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    public class AuthService : IAuthService
    {
        private readonly IMailService mailService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly AccessTokenOptions options;
        private readonly MailSettings mailSettings;

        public AuthService(
            IMailService mailService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IOptions<AccessTokenOptions> options,
            IOptions<MailSettings> mailSettings)
        {
            this.mailService = mailService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.options = options.Value;
            this.mailSettings = mailSettings.Value;
        }

        public async Task<AuthResponse> RefreshTokens(string accessToken, string refreshToken)
        {
            var jwtToken = new JwtSecurityToken(accessToken);
            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor")?.Value;

            var user = await this.userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new RefreshTokensFailedException("Invalid access token");
            }

            var refreshTokenFromDb = await this.userManager.GetAuthenticationTokenAsync(user, "UserTokenProvider", "RefreshToken");
            var isValid = await this.userManager.VerifyUserTokenAsync(user, "UserTokenProvider", "RefreshToken", refreshToken);

            if (!isValid || refreshToken != refreshTokenFromDb)
            {
                throw new RefreshTokensFailedException("Invalid refresh token");
            }

            await this.userManager.RemoveAuthenticationTokenAsync(user, "UserTokenProvider", "RefreshToken");

            var tokens = await this.GetTokens(user);
            return tokens;
        }

        public async Task<User> Register(RegisterUserDTO userDto, string userRole)
        {
            var user = new User { Name = userDto.Name, Email = userDto.Email, UserName = userDto.Email };

            var result = await this.userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
            {
                var message =
                    result.Errors.Any(e => e.Code == "DuplicateUserName")
                        ? "Email is already taken"
                        : string.Join(";", result.Errors.ToList().Select(e => e.Description));
                throw new RegisterFailedException(message ?? string.Empty);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, userRole),
                new Claim(ClaimTypes.Actor, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email!),
            };

            var emailToken = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
            var emailTokenHtmlVersion = HttpUtility.UrlEncode(emailToken);

            var url = $"{this.mailSettings.RedirectUrl}?userId={user.Id}&emailToken={emailTokenHtmlVersion}";

            await this.userManager.AddToRoleAsync(user, userRole);

            var tasks = new Task[]
            {
                this.userManager.AddClaimsAsync(user, claims),
                this.mailService.SendMail(new MailData()
                {
                    EmailSubject = "Email Verification",
                    EmailBody = "Hi, " + user.Name + "!\n" +
                        "You have been sent this email because you created an account on our website. \n" +
                        "Please click on <a href =\"" + url + "\">this link</a> to confirm your email address is correct. ",
                    EmailToId = user.Email,
                    EmailToName = user.Name,
                }),
            };

            await Task.WhenAll(tasks);

            user.PasswordHash = null;

            return user;
        }

        public async Task<AuthResponse> LogIn(string email, string password)
        {
            var user = await this.userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new LogInFailedException("Wrong user");
            }

            var passwordCheckResult = await this.signInManager.CheckPasswordSignInAsync(user, password, false);

            if (!passwordCheckResult.Succeeded)
            {
                throw new LogInFailedException("Wrong password");
            }

            var isEmailConfirmed = await this.userManager.IsEmailConfirmedAsync(user);

            if (!isEmailConfirmed)
            {
                throw new EmailNotConfirmedException("Email not confirmed");
            }

            await this.userManager.RemoveAuthenticationTokenAsync(user, "UserTokenProvider", "RefreshToken");

            var tokens = await this.GetTokens(user);

            return tokens;
        }

        public async Task VerifyMail(string userId, string emailToken)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new EmailVerificationFailedException("User not found");
            }

            var result = await this.userManager.ConfirmEmailAsync(user, emailToken);

            if (!result.Succeeded)
            {
                throw new EmailVerificationFailedException("Invalid email token");
            }
        }

        public async Task<AuthResponse> VerifyGoogle(string token, string userRole = UserRoles.Client)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(token);

                var user = await this.userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    var newUser = new User()
                    {
                        Email = payload.Email,
                        Name = payload.Name,
                        UserName = payload.Email,
                    };

                    var result = await this.userManager.CreateAsync(newUser);

                    if (!result.Succeeded)
                    {
                        throw new RegisterFailedException(string.Join(";", result.Errors.ToList().Select(e => e.Description)));
                    }

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Role, userRole),
                        new Claim(ClaimTypes.Actor, newUser.Id.ToString()),
                        new Claim(ClaimTypes.Email, newUser.Email),
                    };

                    await this.userManager.AddToRoleAsync(newUser, userRole);
                    await this.userManager.AddClaimsAsync(newUser, claims);

                    user = newUser;
                }

                var tokens = await this.GetTokens(user);

                return tokens;
            }
            catch (Exception ex)
            {
                throw new GoogleVerificationFailedException(
                    "Failed to verify Google OAuth token. " + ex.Message, ex);
            }
        }

        private async Task<AuthResponse> GetTokens(User user)
        {
            var newRefreshToken = await this.userManager.GenerateUserTokenAsync(user, "UserTokenProvider", "RefreshToken");
            await this.userManager.SetAuthenticationTokenAsync(user, "UserTokenProvider", "RefreshToken", newRefreshToken);

            var claims = await this.userManager.GetClaimsAsync(user);
            var expiresIn = DateTime.UtcNow.Add(this.options.TokenLifespan);
            var token = this.GetAccessToken(user, claims, expiresIn);

            return new AuthResponse
            {
                AccessToken = token,
                RefreshToken = newRefreshToken,
                ExpiresIn = ((DateTimeOffset)expiresIn).ToUnixTimeSeconds(),
                TokenType = "bearer",
            };
        }

        private string GetAccessToken(User user, IEnumerable<Claim> principal, DateTime expiresIn)
        {
            var claims = principal.ToList();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            var singingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.options.SecretKey ?? string.Empty));

            var jwt = new JwtSecurityToken(
                    issuer: this.options.Issuer,
                    audience: this.options.Audience,
                    claims: claims,
                    expires: expiresIn,
                    notBefore: DateTime.UtcNow,
                    signingCredentials: new SigningCredentials(singingKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}

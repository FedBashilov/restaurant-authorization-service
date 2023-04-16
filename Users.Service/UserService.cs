// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Users.Service
{
    using Infrastructure.Core.Models;
    using Microsoft.AspNetCore.Identity;
    using Users.Service.Exceptions;
    using Users.Service.Models.Responses;

    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;

        public UserService(
            UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<UserResponse> GetUserInfo(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var userInfo = new UserResponse()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };

            return userInfo;
        }
    }
}

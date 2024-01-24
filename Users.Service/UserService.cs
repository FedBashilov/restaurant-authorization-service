// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Users.Service
{
    using CloudStorage.Service.Interfaces;
    using Infrastructure.Core.Models;
    using Microsoft.AspNetCore.Identity;
    using Users.Service.Exceptions;
    using Users.Service.Models.Responses;

    public class UserService : IUserService
    {
        private readonly ICloudStorageService cloudStorageService;
        private readonly UserManager<User> userManager;

        public UserService(
            ICloudStorageService cloudStorageService,
            UserManager<User> userManager)
        {
            this.cloudStorageService = cloudStorageService;
            this.userManager = userManager;
        }

        public async Task<UserResponse> GetUserInfo(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var userInfo = new UserResponse()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ImageUrl = user.ImageUrl,
            };

            return userInfo;
        }

        public async Task<UserResponse> UpdateUserInfo(string userId, UpdateUserInfoDTO updateUserDto)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            user.Name = updateUserDto.Name;

            if (updateUserDto.Image != default && updateUserDto.Image.Length != 0)
            {
                var imageUrl = await this.cloudStorageService.UploadFile(updateUserDto.Image, "user", "/users");
                user.ImageUrl = imageUrl.ToString();
            }

            await this.userManager.UpdateAsync(user);

            var userInfo = new UserResponse()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ImageUrl = user.ImageUrl,
            };

            return userInfo;
        }
    }
}

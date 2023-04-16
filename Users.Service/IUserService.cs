// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Users.Service
{
    using Users.Service.Models.Responses;

    public interface IUserService
    {
        public Task<UserResponse> GetUserInfo(string userId);
    }
}

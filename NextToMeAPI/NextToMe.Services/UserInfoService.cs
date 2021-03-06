﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NextToMe.Common.DTOs;
using NextToMe.Database;
using NextToMe.Database.Entities;
using NextToMe.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NextToMe.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserInfoService(
            ApplicationDbContext dbContext,
            UserManager<User> userManager,
            IHttpContextAccessor contextAccessor)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }
        public async Task ChangeUserInfo(ChangeUserInfoRequest request)
        {
            User user = await _userManager.FindByEmailAsync(_contextAccessor.HttpContext.User.Identity.Name);

            if (request.ImageBase64 != null)
            {
                if (user.UserImage == null)
                {
                    user.UserImage = new UserImage() { Image = request.ImageBase64, User = user };
                }
                else
                {
                    user.UserImage.Image = request.ImageBase64;
                }
            }

            if (request.UserName != null)
            {
                await _userManager.SetUserNameAsync(user, request.UserName);
            }

            await _dbContext.SaveChangesAsync();
        }

        public Task<List<UserInfoResponse>> GetUserInfo(List<Guid> userIds)
        {
            return Task.FromResult(_dbContext
                .Users
                .Where(x => userIds.Contains(x.Id))
                .Select(x => new UserInfoResponse()
                {
                    UserId = x.Id,
                    UserName = x.UserName,
                    ImageBase64 = x.UserImage.Image
                })
                .ToList());
        }
    }
}

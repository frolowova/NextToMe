using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NextToMe.Common.DTOs;
using NextToMe.Common.Exceptions;
using NextToMe.Database;
using NextToMe.Database.Entities;
using NextToMe.Services.ServiceInterfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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

            UserImage userImage = user.UserImage;

            if (request.ImageBase64 != null)
            {
                userImage.Image = request.ImageBase64;
            }

            if (request.UserName != null)
            {
                user.UserName = request.UserName;
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

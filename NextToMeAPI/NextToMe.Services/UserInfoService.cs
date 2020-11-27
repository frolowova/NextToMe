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

namespace NextToMe.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public UserInfoService(
            ApplicationDbContext dbContext,
            UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public async Task ChangeUserInfo(ChangeUserInfoRequest request)
        {
            User user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (user == null)
            {
                throw new AuthException("Incorrect user ID ");
            }

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

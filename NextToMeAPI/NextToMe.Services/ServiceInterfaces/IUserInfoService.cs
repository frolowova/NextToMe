using System;
using System.Collections.Generic;
using NextToMe.Common.DTOs;
using System.Threading.Tasks;

namespace NextToMe.Services.ServiceInterfaces
{
    public interface IUserInfoService
    {
        public Task ChangeUserInfo(ChangeUserInfoRequest request);

        public Task<List<UserInfoResponse>> GetUserInfo(List<Guid> userIds);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextToMe.Common.DTOs;
using NextToMe.Common.Models;
using NextToMe.Services.ServiceInterfaces;

namespace NextToMe.API.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.User)]
    [Route("api/user/information")]
    public class UserInfoController : Controller
    {
        private readonly IUserInfoService _userInfoService;

        public UserInfoController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        [HttpPost]
        [Route("send")]
        public async Task ChangeUserInfo(ChangeUserInfoRequest request)
        {
            await _userInfoService.ChangeUserInfo(request);
        }

        [HttpPost]
        [Route("get")]
        public async Task<List<UserInfoResponse>> GetUserInfo(List<Guid> userIds)
        {
           return await _userInfoService.GetUserInfo(userIds);
        }
    }
}

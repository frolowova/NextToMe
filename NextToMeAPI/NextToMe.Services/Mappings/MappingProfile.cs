using AutoMapper;
using NextToMe.Common.DTOs;
using NextToMe.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NextToMe.Services.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddMessageRequest, Message>().ReverseMap();
            CreateMap<Message, AddMessageResponse>()
                .ForMember(x => x.From, opt => opt.MapFrom(y => y.User.UserName))
                .ReverseMap();

            CreateMap<User, LoginResponse>().ReverseMap();
        }
    }
}

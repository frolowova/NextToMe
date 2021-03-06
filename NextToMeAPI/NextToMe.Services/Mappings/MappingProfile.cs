﻿using System.Linq;
using AutoMapper;
using NetTopologySuite.Geometries;
using NextToMe.Common.DTOs;
using NextToMe.Database.Entities;

namespace NextToMe.Services.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddMessageRequest, Message>()
                .ForMember(x => x.Location, opt => opt.MapFrom(y => new Point(y.Location.Latitude, y.Location.Longitude){SRID = 4326}))
                .ReverseMap();

            CreateMap<Message, MessageResponse>()
                .ForMember(x => x.From, opt => opt.MapFrom(y => y.User.Id))
                .ForMember(x => x.FromName, opt => opt.MapFrom(y => y.User.UserName))
                .ForMember(x => x.Location,
                    opt => opt.MapFrom(y => new Common.Models.Location(y.Location.X, y.Location.Y)))
                .ForMember(x => x.LikesCount, opt => opt.MapFrom(y => y.UserLikedMessages.Count))
                .ForMember(x => x.Photos, opt => opt.MapFrom(y => y.MessageImages.Select(image => image.Id)))
                .ReverseMap();

            CreateMap<AddMessageCommentRequest, MessageComment>().ReverseMap();

            CreateMap<MessageComment, MessageCommentResponse>()
                .ForMember(x => x.From, opt => opt.MapFrom(y => y.User.Id))
                .ForMember(x => x.FromName, opt => opt.MapFrom(y => y.User.UserName))
                .ReverseMap();

            CreateMap<User, LoginResponse>().ReverseMap();
        }
    }
}

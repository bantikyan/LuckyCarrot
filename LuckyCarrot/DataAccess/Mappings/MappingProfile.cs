using AutoMapper;
using DataAccess.Models;
using DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterModel, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.Points, opt => opt.MapFrom(src => 100))
                .ForMember(dest => dest.ReceivedPoints, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTimeOffset.Now));

            CreateMap<PointTransfer, PointTransferModel>();
            CreateMap<PointTransferModel, PointTransfer>();

            CreateMap<Reason, ReasonModel>();
            CreateMap<ReasonModel, Reason>();

            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();
        }
        
    }
}

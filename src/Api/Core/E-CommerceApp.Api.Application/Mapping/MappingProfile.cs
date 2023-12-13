using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_CommerceApp.Api.Domain.Models;
using E_CommerceApp.Common.Infrastructure;
using E_CommerceApp.Common.Models.Queries.User;
using E_CommerceApp.Common.Models.RequestModels.User;

namespace E_CommerceApp.Api.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginUserViewModel>()
                .ReverseMap();
            CreateMap<User, GetUserViewModel>()
                .ReverseMap();
            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<User, CreateUserViewModel>()
                .ReverseMap();
        }
    }
}

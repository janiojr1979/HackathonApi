using AutoMapper;
using Domain.Models.Dto.Requests;
using Domain.Models.Entities;

namespace Infra.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRequest, User>();
        }
    }
}

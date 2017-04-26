using AutoMapper;
using Cmas.BusinessLayers.Users.Entities;
using Cmas.DataLayers.CouchDb.Users.Dtos;

namespace Cmas.DataLayers.CouchDb.Users
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(
                    dest => dest._id,
                    opt => opt.MapFrom(src => src.Id));

            CreateMap<UserDto, User>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src._id))
                .ForMember(
                    dest => dest.Rev,
                    opt => opt.MapFrom(src => src._rev));
        }
    }
}
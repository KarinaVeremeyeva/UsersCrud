using AutoMapper;
using UsersCrud.Api.DTOs;
using UsersCrud.BLL.Models;

namespace UsersCrud.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, UserDto>().ReverseMap();
            CreateMap<UserModel, CreateUserDto>().ReverseMap();
            CreateMap<UserModel, UpdateUserDto>().ReverseMap();
            CreateMap<RoleModel, RoleDto>();
        }
    }
}

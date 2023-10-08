using AutoMapper;
using UsersCrud.Api.DTOs;
using UsersCrud.BLL.Models;

namespace UsersCrud.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, UserDto>();
            CreateMap<UpdateUserDto, UserModel>();
            CreateMap<RoleModel, RoleDto>();
            CreateMap<FilterUsersDto, FilterUsersModel>();
        }
    }
}

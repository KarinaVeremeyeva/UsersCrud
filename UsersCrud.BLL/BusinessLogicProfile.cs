using AutoMapper;
using UsersCrud.BLL.Models;
using UsersCrud.DAL;
using UsersCrud.DAL.Entities;
using UsersCrud.DAL.Enums;

namespace UsersCrud.BLL
{
    public class BusinessLogicProfile : Profile
    {
        public BusinessLogicProfile()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<Role, RoleModel>()
                .ForMember(
                    dest => dest.RoleName,
                    opt => opt.MapFrom(src => src.Name.ToString()));
            CreateMap<RoleModel, Role>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => Enum.Parse<Roles>(src.RoleName)));
            CreateMap<FilterUsers, FilterUsersModel>().ReverseMap();
        }
    }
}

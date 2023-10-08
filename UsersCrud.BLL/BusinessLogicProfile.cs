using AutoMapper;
using UsersCrud.BLL.Models;
using UsersCrud.DAL.Entities;

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
        }
    }
}

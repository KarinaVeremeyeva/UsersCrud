using AutoMapper;
using UsersCrud.BLL.Models;
using UsersCrud.DAL.Repositories;

namespace UsersCrud.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(
            IRoleRepository roleRepository,
            IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<RoleModel> GetRoleByIdAsync(Guid id)
        {
            var role = await _roleRepository.GetByIdAsync(id);

            return _mapper.Map<RoleModel>(role);
        }
    }
}

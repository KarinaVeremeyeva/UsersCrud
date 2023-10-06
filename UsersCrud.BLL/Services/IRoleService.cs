using UsersCrud.BLL.Models;

namespace UsersCrud.BLL.Services
{
    public interface IRoleService
    {
        Task<RoleModel> GetRoleByIdAsync(Guid id);
    }
}

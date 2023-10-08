using UsersCrud.BLL.Models;

namespace UsersCrud.BLL.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetUsersAsync(FilterUsersModel filterUsers);

        Task<UserModel> GetUserByIdAsync(Guid id);

        Task<UserModel> AddUserAsync(UserModel user);

        Task<UserModel> AddRoleToUserAsync(Guid userId, Guid roleId);

        Task<UserModel> UpdateUserAsync(Guid userId, UserModel user);

        Task RemoveUserAsync(Guid id);

        Task<UserModel> GetUserByEmailAsync(string email);
    }
}

using UsersCrud.BLL.Models;
using UsersCrud.DAL.Entities;

namespace UsersCrud.BLL.Services
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetUsers();

        UserModel GetUserById(Guid id);

        UserModel AddUser(UserModel user);

        void AddRoleToUser(Guid userId, Guid roleId);

        UserModel UpdateUser(UserModel user);

        void RemoveUser(Guid id);
    }
}

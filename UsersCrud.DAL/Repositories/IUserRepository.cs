using UsersCrud.DAL.Entities;

namespace UsersCrud.DAL.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllAsync(FilterUsers filterUsers);

        IQueryable<User> GetQuery();
    }
}
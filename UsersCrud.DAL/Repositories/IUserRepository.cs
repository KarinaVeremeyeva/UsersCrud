using UsersCrud.DAL.Entities;

namespace UsersCrud.DAL.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IQueryable<User> GetQuery();
    }
}
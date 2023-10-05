using Microsoft.EntityFrameworkCore;
using UsersCrud.DAL.Entities;

namespace UsersCrud.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override IEnumerable<User> GetAll()
        {
            var users = _context.Users
                .Include(u => u.Roles)
                .ToList();

            return users;
        }

        public override User GetById(Guid id)
        {
            var user = _context.Users
                .Include(u => u.Roles)
                .SingleOrDefault(u => u.Id == id);

            return user;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using UsersCrud.DAL.Entities;

namespace UsersCrud.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<User> GetByIdAsync(Guid id)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .SingleOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public IQueryable<User> GetQuery()
        {
            return _context.Users.Include(u => u.Roles).AsQueryable();
        }
    }
}

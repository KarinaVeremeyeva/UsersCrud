using Microsoft.EntityFrameworkCore;
using UsersCrud.DAL.Entities;
using UsersCrud.DAL.Enums;

namespace UsersCrud.DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetAllAsync(FilterUsers filterUsers)
        {
            var query = _context.Users.AsQueryable();
            if (filterUsers.MinAge.HasValue)
            {
                query = query.Where(q => q.Age >= filterUsers.MinAge.Value);
            }

            if (filterUsers.MaxAge.HasValue)
            {
                query = query.Where(q => q.Age <= filterUsers.MaxAge.Value);
            }

            if (!string.IsNullOrEmpty(filterUsers.NamePart))
            {
                query = query.Where(q => q.Name.Contains(filterUsers.NamePart));
            }

            if (!string.IsNullOrEmpty(filterUsers.EmailPart))
            {
                query = query.Where(q => q.Email.Contains(filterUsers.EmailPart));
            }

            if (filterUsers.UserOrder.HasValue)
            {
                query = filterUsers.UserOrder.Value switch
                {
                    UserOrder.NameDescending => query.OrderByDescending(q => q.Name),
                    UserOrder.AgeDescending => query.OrderByDescending(q => q.Age),
                    UserOrder.EmailDescending => query.OrderByDescending(q => q.Email),
                    UserOrder.NameAscending => query.OrderBy(q => q.Name),
                    UserOrder.AgeAsceding => query.OrderBy(q => q.Age),
                    UserOrder.EmailAscending => query.OrderBy(q => q.Email),
                    _ => query
                };
            }

            if (filterUsers.RoleIds?.Any() == true)
            {
                //query = query.Where(q => filterUsers.RoleIds.All(roleId => q.Roles.Any(userRole => userRole.Id == roleId)));

                var roleCount = filterUsers.RoleIds.Distinct().Count();

                query = query.Where(q => q.Roles
                    .Where(role => filterUsers.RoleIds.Contains(role.Id)).Count() == roleCount);

            }

            const int pageSize = 3;
            var page = filterUsers.Page >= 0 ? filterUsers.Page : 0;

            var users = await query
                .Skip(page * pageSize)
                .Take(pageSize)
                .Include(u => u.Roles)
                .ToListAsync();

            return users;
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

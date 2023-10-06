using Microsoft.EntityFrameworkCore;
using UsersCrud.DAL.Entities;

namespace UsersCrud.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected ApplicationDbContext _context;
        protected DbSet<TEntity> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(entity => entity.Id == id);
            
            return entity;
        }

        public virtual async Task RemoveAsync(Guid id)
        {
            var entity = await _dbSet.SingleOrDefaultAsync(entity => entity.Id == id);
            if (entity == null)
            {
                return;
            }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}

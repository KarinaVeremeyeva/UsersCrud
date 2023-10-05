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

        public virtual TEntity Add(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual TEntity GetById(Guid id)
        {
            var entity = _dbSet.FirstOrDefault(entity => entity.Id == id);
            
            return entity;
        }

        public virtual void Remove(Guid id)
        {
            var entity = _dbSet.SingleOrDefault(entity => entity.Id == id);
            if (entity == null)
            {
                return;
            }

            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public TEntity Update(TEntity entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();

            return entity;
        }
    }
}

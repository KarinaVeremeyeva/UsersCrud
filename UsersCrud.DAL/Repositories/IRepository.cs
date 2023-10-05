using UsersCrud.DAL.Entities;

namespace UsersCrud.DAL.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(Guid id);

        TEntity Add(TEntity entity);

        TEntity Update(TEntity entity);

        void Remove(Guid id);
    }
}

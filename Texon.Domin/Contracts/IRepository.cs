using Texon.Domin.Entities;

namespace Texon.Domin.Contracts
{
    public interface IRepository<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        Task AddAsync(TEntity entity);
        void Remove(TEntity entity);
        void update(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync(IBaseSpecfications<TEntity> specfications);
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity?> GetByIdAsync(TKey id);
        Task<TEntity?> GetAsync(IBaseSpecfications<TEntity> specfications );

        Task<int> CountAsync(IBaseSpecfications<TEntity> specfications);

    }
}

using Texon.Domin.Entities;

namespace Texon.Domin.Contracts
{
    public interface IUnitofWork
    {
        IRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>()
            where TEntity : BaseEntity<Tkey>;
        Task<int> SaveChangesAsync();
    }
}

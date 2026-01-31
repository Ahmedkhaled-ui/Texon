using Texon.Domin.Contracts;
using Texon.Domin.Entities;
using Texon.Persistence.Context;
using Texon.Persistence.Repository;

namespace Texon.Persistence.UnitofWork
{
    public class UnitofWork : IUnitofWork
    {
        private readonly Dictionary<Type, object> repositories = [];
        private readonly TexonContext context;

        public UnitofWork(TexonContext context)
        {
            this.context = context;
        }

        public IRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            var entityname = typeof(TEntity);

            if (repositories.TryGetValue(entityname, out object? value))
                return (IRepository<TEntity, Tkey>)value;

            var repo = new Repository<TEntity, Tkey>(context);
            repositories.Add(entityname, repo);
            return repo;
        }

        public Task<int> SaveChangesAsync()
       => context.SaveChangesAsync();
    }
}

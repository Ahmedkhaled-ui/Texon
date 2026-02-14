using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Texon.Domin.Contracts;
using Texon.Domin.Entities;
using Texon.Persistence.Context;

namespace Texon.Persistence.Repository
{
    public class Repository<TEntity, Tkey>(TexonContext context) :
        IRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public async Task AddAsync(TEntity entity)
      => await context.AddAsync(entity);

        public Task<int> CountAsync(IBaseSpecfications<TEntity> specfications)
       => context.Set<TEntity>().Specfications(specfications).CountAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync()
     => await context.Set<TEntity>().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync(IBaseSpecfications<TEntity> specfications)
        =>await context.Set<TEntity>().Specfications(specfications).ToListAsync();
        public async Task<TEntity?> GetAsync(IBaseSpecfications<TEntity> specfications)
        => await context.Set<TEntity>().Specfications(specfications).FirstOrDefaultAsync();

       
        public async Task<TEntity?> GetByIdAsync(Tkey id)
    =>  await context.Set<TEntity>().FindAsync(id);

        public void Remove(TEntity entity)
       => context.Remove(entity);

        public void update(TEntity entity)
       => context.Update(entity);

      
    }
}

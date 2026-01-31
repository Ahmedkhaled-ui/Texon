using Microsoft.EntityFrameworkCore;
using Texon.Domin.Contracts;

namespace Texon.Persistence.Repository
{
    public static class SpecficationEvaluator
    {
        public static IQueryable<TEntity> 
            Specfications<TEntity>( this IQueryable<TEntity> inputQuery,
            IBaseSpecfications<TEntity> spec) where TEntity : class
        {
            IQueryable<TEntity> query = inputQuery;

            if(spec.Criteria != null)
                query =query.Where(spec.Criteria);

            if(spec.Includes != null)
                query = spec .Includes.Aggregate(query, (current, include) => current.Include(include));

            if(spec.ApplyOrderBy != null)
                query = query.OrderBy(spec.ApplyOrderBy);

            if(spec.ApplyOrderByDescending != null)
                query = query.OrderByDescending(spec.ApplyOrderByDescending);
            if(spec.IsPagineted)
                query = query.Skip(spec.Skip).Take(spec.Take);
            return query;
        }



    }
}

using System.Linq.Expressions;

namespace Texon.Domin.Contracts
{
    public interface IBaseSpecfications<TEntity> where TEntity : class
    {

        Expression<Func<TEntity, bool>>? Criteria { get; }
        ICollection<Expression<Func<TEntity, object>>> Includes { get; }

        Expression<Func<TEntity, object>> ApplyOrderBy { get; }
        Expression<Func<TEntity, object>> ApplyOrderByDescending { get; }

        int Skip { get; }
        int Take { get; }
        bool IsPagineted { get; }

    }
}

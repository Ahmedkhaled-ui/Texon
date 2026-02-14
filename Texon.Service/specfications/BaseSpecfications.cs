using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Texon.Domin.Contracts;

namespace Texon.Service.specfications
{
    public abstract class BaseSpecfications<TEntity> :
        IBaseSpecfications<TEntity> where TEntity : class
    {

       protected BaseSpecfications(Expression<Func<TEntity , bool>>? criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        public ICollection<Expression<Func<TEntity, object>>>? Includes { get; private set; } = [];

        public Expression<Func<TEntity, object>> ApplyOrderBy { get; private set; }

        public Expression<Func<TEntity, object>> ApplyOrderByDescending { get; private set; }
        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPagineted { get; private set; }


        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
     => Includes.Add(includeExpression);


        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
            => ApplyOrderBy = orderByExpression;

        protected void AddOrderByDes(Expression<Func<TEntity, object>> orderByDescExpression)
            => ApplyOrderByDescending = orderByDescExpression;

        protected void ApplyPagintion (int pageSize , int pageIndex)
        {
            IsPagineted = true;

            Skip = pageSize * (pageIndex - 1);
            Take = pageSize;
        }
    }
}

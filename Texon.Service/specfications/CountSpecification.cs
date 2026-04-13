using System.Linq.Expressions;
using Texon.Service.specfications;

public class CountSpecification<TEntity> : BaseSpecfications<TEntity> where TEntity : class
{
    public CountSpecification() : base(null)
    {

    }
}
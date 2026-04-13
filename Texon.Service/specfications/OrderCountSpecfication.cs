using System.Linq.Expressions;
using Texon.Domin.Entities.Order;
using Texon.Shared.OrderDto;
using Texon.Shared.ProductDto;

namespace Texon.Service.specfications
{
    public class OrderCountSpecfication(OrderQuary quary ): BaseSpecfications<Order>(CreateCriteria(quary))

    {
        private static Expression<Func<Order, bool>> CreateCriteria(OrderQuary query)
        {
            return x =>
                (string.IsNullOrEmpty(query.Email) || x.UserEmail == query.Email) &&
                (string.IsNullOrEmpty(query.Status) || x.Status.ToString() == query.Status) &&
                (string.IsNullOrEmpty(query.Search) || x.UserEmail.Contains(query.Search));
        }
    }
}

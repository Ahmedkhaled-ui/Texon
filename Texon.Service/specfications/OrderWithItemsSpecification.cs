using System.Linq.Expressions;
using Texon.Domin.Entities.Order;
using Texon.Shared.OrderDto;
using Texon.Shared.ProductDto;

namespace Texon.Service.specfications
{
    public class OrderWithItemsSpecification : BaseSpecfications<Order>
    {
        public OrderWithItemsSpecification(OrderQuary query)
        : base(CreateCriteria(query))
        {
            // إضافة الـ Includes عشان الأدمن يشوف كل حاجة
            AddInclude(x => x.OrderItems);
            AddInclude(x => x.DeliveryMethod);

            // ترتيب الأوردرات من الأحدث للأقدم
            AddOrderByDes(x => x.OrderDate);

            // لو عندك Pagination
            // ApplyPaging(query.PageSize * (query.PageIndex - 1), query.PageSize);
            ApplyPagintion(query.pageSize, query.pageIndex);

        }

        private static Expression<Func<Order, bool>> CreateCriteria(OrderQuary query)
        {
            return x =>
                (string.IsNullOrEmpty(query.Email) || x.UserEmail == query.Email) &&
                (string.IsNullOrEmpty(query.Status) || x.Status.ToString() == query.Status);
        }
    }
}

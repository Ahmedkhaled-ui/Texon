using Texon.Domin.Entities.Order;

namespace Texon.Service.specfications
{
    public class OrderWithIdAndEmailSpecfications : BaseSpecfications<Order>
    {
        public OrderWithIdAndEmailSpecfications(Guid id , string Email)
            : base(x => x.Id == id && x.UserEmail == Email)
        {
            AddInclude(x => x.OrderItems);
        }
    }
}

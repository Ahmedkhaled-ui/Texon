using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Texon.Domin.Entities.DeliveryMethod;

namespace Texon.Domin.Entities.Order
{
    public class Order : BaseEntity<Guid>
    {
        public string UserEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;

        public OrderAddress Address { get; set; }

        public DeliveryMethods DeliveryMethod { get; set; }
        public int? DeliveryMethodID { get; set; }

        // السعر اللي العميل وافق عليه وقت الشراء (Snapshot)
        public decimal ShippingCost { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.pending;

        // الإجمالي بيتحسب بناءً على القيم المخزنة فعلياً في جدول الأوردر
        public decimal GetTotal() => SubTotal + ShippingCost;

    }
    [Owned]
    public class OrderAddress
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [Required]
        [Phone]
        [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "رقم الهاتف المصري غير صحيح")]
        public string PhoneNumber { get; set; }
    }
}
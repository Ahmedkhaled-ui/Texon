namespace Texon.Domin.Entities.DeliveryMethod
{
    public class DeliveryMethods : BaseEntity<int>
    {
#nullable disable

        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Cost { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

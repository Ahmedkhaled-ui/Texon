using System.ComponentModel;

namespace Texon.Shared.DeliveryMethodDto
{
    public class DeliveryMethodDto
    {
#nullable disable
        [ReadOnly(true)]
        public int Id { get; set; }

        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Cost { get; set; }
        public bool IsActive { get; set; } 
    }
}

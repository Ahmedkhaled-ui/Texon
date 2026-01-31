using System.ComponentModel.DataAnnotations;

namespace Texon.Domin.Entities.Products
{
    public class Category : BaseEntity<int>
    {
#nullable disable

        [Required(ErrorMessage = "اسم القسم بالعربي مطلوب")]
        [MaxLength(100)]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "English category name is required")]
        [MaxLength(100)]
        public string NameEn { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
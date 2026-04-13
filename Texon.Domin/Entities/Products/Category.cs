using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Texon.Domin.Entities.Products
{
    public class Category : BaseEntity<int>
    {
#nullable disable
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "اسم القسم بالعربي مطلوب")]
        [MaxLength(100)]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "English category name is required")]
        [MaxLength(100)]
        public string NameEn { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
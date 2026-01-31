using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Texon.Domin.Entities;
using Texon.Domin.Entities.Products;

public class Product : BaseEntity<int>
{
#nullable disable

    // --- الأسماء باللغتين ---
    [Required(ErrorMessage = "اسم المنتج بالعربي مطلوب")]
    [MaxLength(256)]
    public string NameAr { get; set; }

    [Required(ErrorMessage = "English product name is required")]
    [MaxLength(256)]
    public string NameEn { get; set; }


    // --- الوصف باللغتين ---
    public string DescriptionAr { get; set; }
    public string DescriptionEn { get; set; }


    // --- البيانات الأساسية (لا تتغير بتغير اللغة) ---
    [MaxLength(2048)]
    [Required(ErrorMessage = "صورة المنتج مطلوبة")]
    public string PhotoUrl { get; set; }
    public  ICollection<ProductImages> Images { get; set; } = new HashSet<ProductImages>();
    [Range(0, 999999.99, ErrorMessage = "السعر يجب أن يكون قيمة موجبة")]
    public decimal Price { get; set; }

    public decimal? DiscountPrice { get; set; }
    public int StockQuantity { get; set; }

    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsVisible { get; set; } = true;


    // --- الخواص المحسوبة (Calculated Properties) ---
    [NotMapped]
    public bool IsOnSale => DiscountPrice.HasValue && DiscountPrice < Price;

    [NotMapped]
    public int DiscountPercentage => IsOnSale
        ? (int)((Price - DiscountPrice.Value) / Price * 100)
        : 0;

    [NotMapped]
    public bool IsInStock => StockQuantity > 0;
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Texon.Domin.Entities.Products;

namespace Texon.Persistence.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // --- إعدادات الأسماء ---
            builder.Property(p => p.NameAr)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnType("nvarchar");

            builder.Property(p => p.NameEn)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnType("nvarchar");

            // --- إعدادات الوصف ---
            builder.Property(p => p.DescriptionAr)
                .HasColumnType("nvarchar(max)");

            builder.Property(p => p.DescriptionEn)
                .HasColumnType("nvarchar(max)");

            // --- الإعدادات المالية ---
            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.DiscountPrice)
                .HasColumnType("decimal(18,2)");

            // --- إعدادات الصورة والوقت ---
            builder.Property(p => p.PhotoUrl)
                .IsRequired()
                .HasMaxLength(2048)
                .HasColumnType("nvarchar");

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // --- العلاقات ---
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // يفضل عشان متدلتش التصنيف بالغلط فيتمسح كل منتجاته
        }
    }
}
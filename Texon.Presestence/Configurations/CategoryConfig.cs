using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Texon.Domin.Entities.Products;

namespace Texon.Persistence.Configurations
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.NameAr)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnType("nvarchar");

            builder.Property(c => c.NameEn)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnType("nvarchar");

           
        }
    }
}
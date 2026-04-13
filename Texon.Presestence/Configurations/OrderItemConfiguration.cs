using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Texon.Domin.Entities.Order;

namespace Texon.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(oi => oi.Price)
                        .HasColumnType("decimal(18,2)");

            builder.Property(oi => oi.ProductName)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(oi => oi.photo)
                .IsRequired(false);
        }
    }
}

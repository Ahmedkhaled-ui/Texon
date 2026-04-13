using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Texon.Domin.Entities.Order;

namespace Texon.Persistence.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.SubTotal)
                        .HasColumnType("decimal(18,2)");
            builder.HasOne(o => o.DeliveryMethod)
            .WithMany()
            .HasForeignKey(o => o.DeliveryMethodID)
            .OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(o => o.OrderItems)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
            builder.Property(o => o.UserEmail).IsRequired().HasMaxLength(150);
          

        }
    }
}

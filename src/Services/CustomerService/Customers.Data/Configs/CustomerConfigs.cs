using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Customers.Domain.Entities;

namespace Customers.Data.Configs
{
    internal class CustomerConfigs : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(50);
            builder.Property(e => e.Phone)
            .IsRequired()
            .HasMaxLength(15);
            builder.OwnsOne(i => i.Name, f =>
            {
                f.Property(n => n.Sur).IsRequired().HasMaxLength(20);
                f.Property(n => n.First).IsRequired().HasMaxLength(20);
                f.Property(n => n.Middle).HasMaxLength(20);
            });
        }
    }
}

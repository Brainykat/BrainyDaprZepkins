using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Customers.Domain.Entities;
using SharedBase.ValueObjects;

namespace Customers.Data.Configs
{
    public class CustomerConfigs : IEntityTypeConfiguration<Customer>
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
            //Seeding
            builder.HasData(
                Customer.Create(Name.Create("Kariuki", "Peter", "Sawa"), "+254721937200", "chris@brainykat.com"),
                Customer.Create(Name.Create("Kibaki", "Mwai"), "+254721007200", "kibaki@brainykat.com")
                );
        }
    }
}

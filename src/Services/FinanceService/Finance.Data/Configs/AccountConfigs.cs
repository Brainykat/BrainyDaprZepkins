using Finance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Data.Configs
{
    public class AccountConfigs : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(e => e.Name)
                .HasMaxLength(100)
      .IsRequired();
            builder.Property(e => e.AccountNumber)
                .HasMaxLength(25)
            .IsRequired();
            builder.OwnsOne(i => i.AccountTransactionLimit, f =>
            {
                f.Property(n => n.Currency).IsRequired().HasMaxLength(6);
                f.Property(n => n.Amount).IsRequired().HasColumnType("decimal(18,4)");
                f.Property(n => n.Time).IsRequired();
            });
            
            
        }
    }
}

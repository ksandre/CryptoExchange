using CryptoExchange.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoExchange.Persistence.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasData(
                new Currency
                {
                    Id = 1,
                    Name = "USDT",
                    DateCreated = DateTime.Now.ToUniversalTime(),
                    DateModified = DateTime.Now.ToUniversalTime()
                },
                new Currency
                {
                    Id = 2,
                    Name = "BTC",
                    DateCreated = DateTime.Now.ToUniversalTime(),
                    DateModified = DateTime.Now.ToUniversalTime()
                }
            );

            builder.Property(q => q.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}

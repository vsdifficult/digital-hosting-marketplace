using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostMarket.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HostMarket.Infrastructure.Data.EntityFramework.Configurations
{
    public class TariffConfiguartion : IEntityTypeConfiguration<TariffEntity>
    {
        public void Configure(EntityTypeBuilder<TariffEntity> builder)
        {
            builder.ToTable("Tariffs");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TariffName)
                .IsRequired();

            builder.Property(x => x.RamGb)
                .IsRequired();

            builder.Property(x => x.DiskGb)
                .IsRequired();

            builder.Property(x => x.Price)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.CreateAt)
                .IsRequired();

            builder.Property(x => x.UpdateAt)
                .IsRequired();

            builder.HasMany(x => x.Servers)
                .WithOne(x => x.Tariff)
                .IsRequired();
        }
    }
}

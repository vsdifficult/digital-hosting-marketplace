using HostMarket.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HostMarket.Infrastructure.Data.EntityFramework.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.CreateAt)
                .IsRequired();

            builder.Property(t => t.UpdateAt)
                .IsRequired();

            builder.Property(t => t.Status)
                .IsRequired();

            builder.Property(t => t.userId).IsRequired();
            builder.Property(t => t.serverId).IsRequired();
            builder.Property(t => t.Amount).IsRequired();
            builder.Property(t => t.transactionDate).IsRequired();
            builder.HasOne(t => t.User).WithMany(t => t.Transactions).IsRequired();
        }
    }
}
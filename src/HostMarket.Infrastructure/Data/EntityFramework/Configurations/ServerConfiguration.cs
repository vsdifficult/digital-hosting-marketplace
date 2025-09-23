﻿using HostMarket.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HostMarket.Infrastructure.Data.EntityFramework.Configurations
{
    public class ServerConfiguration : IEntityTypeConfiguration<ServerEntity>
    {
        public void Configure(EntityTypeBuilder<ServerEntity> builder)
        {
            builder.ToTable("Servers");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.ownerId)
                .IsRequired();

            builder.Property(s => s.CreateAt)
                .IsRequired();

            builder.Property(s => s.UpdateAt)
                .IsRequired();

            builder.Property(s => s.Status)
                .IsRequired();

            builder.Property(s => s.ServerName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(s => s.Description)
                .HasMaxLength(400)
                .IsRequired();

            builder.Property(s => s.Price)
                .IsRequired();

            builder.Property(s => s.ServStatus)
                .IsRequired();

            builder.HasOne(s => s.User)
                .WithMany(u => u.Servers)
                .IsRequired();
        }
    }
}

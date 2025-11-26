using HostMarket.Infrastructure.Data.Entities;
using HostMarket.Infrastructure.Data.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Infrastructure.Data.EntityFramework
{
    public class DataContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<ServerEntity> Servers { get; set; } = null!;
        public DbSet<TransactionEntity> Transactions { get; set; } = null!;
        public DbSet<TariffEntity> Tariffs { get; set; } = null!;
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("hostmarket");
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ServerConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new TariffConfiguartion());

        }
    }
}

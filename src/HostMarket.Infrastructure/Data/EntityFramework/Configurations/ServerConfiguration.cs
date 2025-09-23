using HostMarket.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HostMarket.Infrastructure.Data.EntityFramework.Configurations
{
    public class ServerConfiguration : IEntityTypeConfiguration<ServerEntity>
    {
        public void Configure(EntityTypeBuilder<ServerEntity> builder)
        {

        }
    }
}

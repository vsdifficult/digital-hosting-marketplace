
using Microsoft.EntityFrameworkCore; 
using HostMarket.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HostMarket.Infrastructure.Data.EntityFramework.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            
        }
    }
}


using Microsoft.EntityFrameworkCore; 
using HostMarket.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HostMarket.Infrastructure.Data.EntityFramework.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.CreateAt)
                .IsRequired();

            builder.Property(u => u.UpdateAt)
                .IsRequired();

            builder.Property(u => u.Status)
                .IsRequired();

            builder.Property(u => u.UserName)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(u => u.Password)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(u => u.Balance)
                .HasDefaultValue(0);

            builder.Property(u => u.Code)
                .HasMaxLength(5);

            builder.Property(u => u.IsVerify)
                .HasDefaultValue(false);

            builder.Property(u => u.RegistrationDate)
                .IsRequired();

            builder.HasMany(u => u.Servers)
                .WithOne(s => s.User)
                .IsRequired();  
            
            builder.HasMany(u => u.Transactions)
                .WithOne(s => s.User)
                .IsRequired(); 
        }
    }
}



using HostMarket.Shared.Models;

namespace HostMarket.Infrastructure.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public UserRole Role { get; set; }
        public string Code { get; set; } 
        public bool IsVerify { get; set; }
        public DateTime RegistrationDate { get; set; }

        // Navigation 

        public ICollection<ServerEntity> Servers { get; set; } = new List<ServerEntity>(); 

        public ICollection<TransactionEntity> Transactions { get; set; } = new List<TransactionEntity>(); 
    }
}

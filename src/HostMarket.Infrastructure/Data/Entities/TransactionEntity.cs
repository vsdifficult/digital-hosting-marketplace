
namespace HostMarket.Infrastructure.Data.Entities;

public class TransactionEntity : BaseEntity
{
    public Guid userId { get; set; }

    public Guid serverId { get; set; }

    public decimal Amount { get; set; }

    public DateTime transactionDate { get; set; } 

    // Navigation 
        
    public UserEntity? User { get; set; }
}
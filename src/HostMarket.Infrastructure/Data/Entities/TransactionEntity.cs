namespace HostMarket.Infrastructure.Entities;

public class TransactionEntity : BaseEntity
{
    public Guid userId { get; set; }

    public Guid serverId { get; set; }

    public DateTime transactionDate { get; set; }
}

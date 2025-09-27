namespace HostMarket.Shared.Dto;

public record TransactionDto
{
    public Guid userId { get; init; }
    public Guid serverId { get; set; }
    public decimal Amount { get; init; }
    public DateTime transactionDate { get; init; }
}
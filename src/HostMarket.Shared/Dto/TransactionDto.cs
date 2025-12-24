using HostMarket.Shared.Models;

namespace HostMarket.Shared.Dto;

public record TransactionDto
{
    public Guid Id { get; set; }
    public Guid userId { get; init; }
    public Guid serverId { get; set; }
    public decimal Amount { get; init; }
    public DateTime transactionDate { get; init; }
    public TransactionStatus transactionStatus { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }

    public Status Status { get; set; }
}
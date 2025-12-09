using HostMarket.Infrastructure.Data.DTO;
using HostMarket.Shared.Dto;
using HostMarket.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static HostMarket.Core.Services.Interfaces.IAdminBFFService;

namespace HostMarket.Core.Services.Interfaces
{
    public record ServerResult
    {
        public string? Ip { get; init; }
        public int? Port { get; init; }
        public ServerStatus? Status { get; init; }
        public string? ErrorMessage { get; set; }
    }
    public interface IServerBFFService
    {
        Task<ServerResult> ServerRentalAsync(Guid userId, Guid serverId);
        Task<bool> ConfirmTransactionAsync(Guid transactionId);
        Task<bool> ResetLease(ServerDTO server);
        // Task<ServerResult> HealthCheckAsync(Guid serverId);
    }
}

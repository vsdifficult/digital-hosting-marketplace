using HostMarket.Infrastructure.Data.DTO;
using HostMarket.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static HostMarket.Core.Services.Interfaces.IAdminBFFService;

namespace HostMarket.Core.Services.Interfaces
{
    public interface IServerBFFService
    {
        Task<ServerResult> ServerRentalAsync(Guid userId, Guid serverId);
        Task<Guid> MakeTransactionAsync(Guid userId, decimal amount, Guid serverId);
        Task<AdminResult> ConfirmTransactionAsync(Guid transactionId);
    }
}

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostMarket.Infrastructure.Data.DTO;
using HostMarket.Shared.Dto;
using HostMarket.Shared.Models;

namespace HostMarket.Core.Services.Interfaces;

public interface IServerBFFService
{
    Task<ServerResult> HealthCheckAsync(Guid serverId);
    Task<ServerResult> ServerRentalAsync(Guid userId, Guid serverId);
    Task<AdminResult> ConfirmTransactionAsync(Guid transactionId);  
}



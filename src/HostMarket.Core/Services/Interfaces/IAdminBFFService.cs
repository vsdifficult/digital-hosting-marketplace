using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostMarket.Infrastructure.Data.DTO;
using HostMarket.Shared.Dto;
using HostMarket.Shared.Models;

namespace HostMarket.Core.Services.Interfaces;

public interface IAdminBFFService
{
    // Just creating a seerver
    Task<Guid?> CreateServerAsync();
    Task<IEnumerable<ServerDTO>> GetAllServersAsync();
    Task<bool> UpdateServerInfoAsync(Guid serverID);
    Task<bool> DeleteServerAsync(Guid serverId);
}

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostMarket.Infrastructure.Data.DTO;
using HostMarket.Shared.Dto;
using HostMarket.Shared.DTO;
using HostMarket.Shared.Models;

namespace HostMarket.Core.Services.Interfaces;

public interface IAdminBFFService
{
    // Just creating a seerver
    Task<Guid?> CreateServerAsync(CreateServerDTO serverDTO);
    Task<IEnumerable<ServerDTO>> GetAllServersAsync();
    Task<bool> UpdateServerInfoAsync(Guid serverID);
    Task<bool> DeleteServerAsync(Guid serverId);
    Task<ServerStatus?> GetServerStatusAsync(Guid serverId);
    Task<Guid?> CreateTariffAsync(CreateTariffDto createTariffDto);
    Task<bool> UpdateTariffAsync(Guid tariffId);
    Task<bool> DeleteTariffAsync(Guid tariffId);
    Task<IEnumerable<TariffDto>> GetAllTariffsAsync();
}

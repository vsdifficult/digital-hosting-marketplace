using HostMarket.Core.Services.Interfaces;
using HostMarket.Shared.Dto;
using HostMarket.Infrastructure.Data;
using HostMarket.Infrastructure.Data.DTO;
using HostMarket.Shared.Models;
using HostMarket.Core.Repositories;
using System.Data.Common;

namespace HostMarket.Core.Services.Implementations.Bff;

public class AdminBFFService : IAdminBFFService
{
    private readonly IServerRepository _serverRepository;

    public AdminBFFService(IServerRepository serverRepository)
    {
        _serverRepository = serverRepository;
    }

    // Creating Server function
    public async Task<Guid> CreateServerAsync()
    {
        // Creating a new ServerDto
        var serverId = Guid.NewGuid();
        var server = new ServerDTO
        {
            Id = serverId,
            ServerName = "Server №" + Convert.ToString(serverId),
            Description = string.Empty,
            Price = 0,      // ♦ update
            ServStatus = ServerStatus.Available,
            CreateAt = DateTime.Now,
            UpdateAt = DateTime.Now,
            Status = Status.Active
        };

        // return serverId
        return serverId;
    }

    // Get Servers ID function
    public async Task<IEnumerable<ServerDTO>> GetServersIdAsync()
    {
        // getting the server dto
        var servers = await _serverRepository.GetAllAsync();
        // return list of servers
        return servers;
    }

    // Update Server info
    public async Task<bool> UpdateServerInfoAsync(Guid serverId)
    {
        // try to get serverDto
        var serverDto = await _serverRepository.GetByIdAsync(serverId);

        if (serverDto != null)
        {
            // updating server info 
            await _serverRepository.UpdateAsync(serverDto);
            return true;
        }
        else
        {
            return false;
        }
    }
    
    
    // Delete server 
    public async Task<bool> DeleteServerAsync(Guid serverId)
    {
        try
        {
            await _serverRepository.DeleteAsync(serverId);
            return true;
        }

        catch
        {
            return false;
        }
    }

}

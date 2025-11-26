using HostMarket.Core.Services.Interfaces;
using HostMarket.Shared.Dto;
using HostMarket.Infrastructure.Data;
using HostMarket.Infrastructure.Data.DTO;
using HostMarket.Shared.Models;
using HostMarket.Core.Repositories;
using System.Data.Common;
using System.Net.NetworkInformation;

namespace HostMarket.Core.Services.Implementations.Bff;

public class AdminBFFService : IAdminBFFService
{
    private readonly IDataService _dataService; 

    public AdminBFFService(IDataService dataService)
    {
        _dataService = dataService; 
    }

    // Creating Server function
    public async Task<AdminResult> CreateServerAsync()
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
        await _dataService.Servers.CreateAsync(server);  
        var dict = new Dictionary<string, string>(); 
        dict.Add("IP", "1111"); 

        return new AdminResult {
            Success = true,
            Data = dict
            }; 
    }

    // Get Servers ID function
    public async Task<AdminResult> GetAllServersAsync()
    {
        // getting the server dto
        var servers = await _dataService.Servers.GetAllAsync();

        // Data dict
        var dict = new Dictionary<string, IEnumerable<ServerDTO>>
        {
            ["Servers"] = servers  
        };

        // return list of servers
        return new AdminResult {
            Success = true,
            DataList = dict    
            };
    }

    // Update Server info
    public async Task<AdminResult> UpdateServerInfoAsync(Guid serverId)
    {
        // try to get serverDto
        var serverDto = await _dataService.Servers.GetByIdAsync(serverId);
        
        if (serverDto != null)
        {
            // updating server info 
            await _dataService.Servers.UpdateAsync(serverDto);
            return new AdminResult {
                Success = true,
                Message = "The server info has been upgraded successfully."
                };

        }

        else
        {
            return new AdminResult {
                Success = false,
                Message = "The server cannot be found."
                };
        }

    }


    // Delete server 
    public async Task<AdminResult> DeleteServerAsync(Guid serverId)
    {
        try
        {
            await _dataService.Servers.DeleteAsync(serverId);
            return new AdminResult {
                Success = true,
                Message = "The server has been deleted successfully."
                };
        }

        catch
        {
            return new AdminResult {
                Success = false,
                ErrorMessage = "The server cannot be deleted."
                };
        }
    }

    // Check: server state
    public async Task<ServerResult> GetServerStatusAsync(Guid serverId)
    {
        var server = await _dataService.Servers.GetByIdAsync(serverId);

        // if server==null -> throw Exceprion
        if (server == null)
        {
            throw new Exception("Server cannot be found.");
        }

        // Else -> return servStatus
        return new ServerResult {
            Status = server.ServStatus
            }; 
    }
    
    // Ping Server
    public async Task<ServerResult> HealthCheckByPingAsync(Guid serverId)
    {
        var server = await _dataService.Servers.GetByIdAsync(serverId);

        // // if server==null -> throw Exceprion
        if (server == null) throw new Exception("Server cannot be found.");

        // try to ping the server
        var server_address = server.Address;

        try
        {
        // if the server was found, we return the status
        Ping ping = new Ping();
        PingReply pingReply = ping.Send(server_address, 1000);  // Time-out for a 1000 second

        // Check for the server request        
        if (pingReply.Status == IPStatus.Success)
        {
            return new ServerResult{
                Status = ServerStatus.Available  
                };
        }
        else
        {
            return new ServerResult{
                Status = ServerStatus.Purchased
                };
        }
        }

        catch
        {
            return new ServerResult{
                ErrorMessage = "Server cannot be Ping"  
                };
        }
    }

}

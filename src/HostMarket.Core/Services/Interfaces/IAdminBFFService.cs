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

public record ServerResult
{
    public string Ip {get;init;} 
    public int Port {get;init;}
}
public record AdminResult
{
    public bool Success {get;init;} 
    public string Message {get;init;} 

    public Dictionary<string, string> Data {get;init;}
} 

public interface IAdminBFFService
{
    // Just creating a seerver
    Task<AdminResult> CreateServerAsync();
    Task<IEnumerable<ServerDTO>> GetAllServersAsync();
    Task<bool> UpdateServerInfoAsync(Guid serverID);
    Task<bool> DeleteServerAsync(Guid serverId);
    Task<ServerStatus?> GetServerStatusAsync(Guid serverId);
}

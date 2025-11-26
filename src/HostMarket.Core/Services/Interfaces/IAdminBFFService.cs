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
    public ServerStatus Status {get; init;}
    public string? ErrorMessage {get; set;}
}

public record AdminResult
{
    public bool Success {get;init;} 
    public string? Message {get;init;} 
    public string? ErrorMessage {get;init;}

    public Dictionary<string, string>? Data {get;init;}
    public Dictionary<string, IEnumerable<ServerDTO>>? DataList {get;init;}

    public TransactionStatus transactionStatus {get;init;}
} 

public interface IAdminBFFService
{
    // Just creating a seerver
    Task<AdminResult> CreateServerAsync();
    Task<AdminResult> GetAllServersAsync();
    Task<AdminResult> UpdateServerInfoAsync(Guid serverID);
    Task<AdminResult> DeleteServerAsync(Guid serverId);
    Task<ServerResult> GetServerStatusAsync(Guid serverId);
    Task<ServerResult> HealthCheckByPingAsync(Guid serverId);
}

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

public record ServerResult
{
    public string Ip {get;init;} 
    public int Port {get;init;} 
    public ServerStatus Status {get; init;} 
}
public record AdminResult<T>
{
    public bool Success {get;init;} 
    public string Message {get;init;} 
    public string IP {get;init;}
    public int Port {get;init;} 
    public string ErrorMessage {get;init;}  

    public List<T>? DataList {get;init;}
} 

public interface IAdminBFFService
{

    // Just creating a seerver
    Task<AdminResult<object>> CreateServerAsync(CreateServerDTO serverDTO);
    Task<AdminResult<IEnumerable<ServerDTO>>> GetAllServersAsync();
    Task<AdminResult<object>> UpdateServerInfoAsync(Guid serverID);
    Task<AdminResult<object>> DeleteServerAsync(Guid serverId);
    Task<ServerResult> GetServerStatusAsync(Guid serverId);
    Task<AdminResult<object>> CreateTariffAsync(CreateTariffDto createTariffDto);
    Task<AdminResult<object>> UpdateTariffAsync(Guid tariffId);
    Task<AdminResult<object>> DeleteTariffAsync(Guid tariffId);
    Task<AdminResult<IEnumerable<TariffDto>>> GetAllTariffsAsync();
}

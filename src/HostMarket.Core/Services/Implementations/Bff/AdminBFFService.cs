using HostMarket.Core.Repositories;
using HostMarket.Core.Services.Interfaces;
using HostMarket.Infrastructure.Data;
using HostMarket.Infrastructure.Data.DTO;
using HostMarket.Shared.Dto;
using HostMarket.Shared.DTO;
using HostMarket.Shared.Models;
using System;
using System.Data.Common;

namespace HostMarket.Core.Services.Implementations.Bff;

public class AdminBFFService : IAdminBFFService
{
    private readonly IDataService _dataService;

    public AdminBFFService(IDataService dataService)
    {
        _dataService = dataService;
    }

    // Creating Server function
    public async Task<Guid?> CreateServerAsync(CreateServerDTO createDTO)
    {
        // Creating a new ServerDto
        var selectedTariff = await _dataService.Tariffs.GetByIdAsync(createDTO.TariffId) ?? throw new Exception("Тариф не найден");
        var serverId = Guid.NewGuid();
        var server = new ServerDTO
        {
            Id = serverId,
            ServerName = createDTO.ServerName,
            Description = createDTO.Description,
            TariffId = createDTO.TariffId,
            Price = selectedTariff.Price,
            ServStatus = ServerStatus.Available,
            CreateAt = DateTime.Now,
            UpdateAt = DateTime.Now,
            Status = Status.Active
        };
        // return serverId
        return await _dataService.Servers.CreateAsync(server);
        
    }

    // Get Servers ID function
    public async Task<IEnumerable<ServerDTO>> GetAllServersAsync()
    {
        // getting the server dto
        var servers = await _dataService.Servers.GetAllAsync();
        // return list of servers
        return servers;
    }

    // Update Server info
    public async Task<bool> UpdateServerInfoAsync(Guid serverId)
    {
        // try to get serverDto
        var serverDto = await _dataService.Servers.GetByIdAsync(serverId);

        if (serverDto != null)
        {
            // updating server info 
            await _dataService.Servers.UpdateAsync(serverDto);
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
            await _dataService.Servers.DeleteAsync(serverId);
            return true;
        }

        catch
        {
            return false;
        }
    }

    // Check: server state
    public async Task<ServerStatus?> GetServerStatusAsync(Guid serverId)
    {
        var server = await _dataService.Servers.GetByIdAsync(serverId);

        // if the server was found, we return the status
        if (server != null)
            return server.ServStatus;

        return null;
    }


    //actions with the tariff

    public async Task<Guid?> CreateTariffAsync(CreateTariffDto createTariffDto)
    {
        var tariff = new TariffDto
        {
            Id = Guid.NewGuid(),
            TariffName = createTariffDto.TariffName,
            RamGb = createTariffDto.RamGb,
            DiskGb = createTariffDto.DiskGb,
            Status = createTariffDto.Status,
            Price = createTariffDto.Price,
            CreateAt = DateTime.UtcNow,
            UpdateAt = DateTime.UtcNow
        };

        return await _dataService.Tariffs.CreateAsync(tariff);
    }

    public async Task<bool> UpdateTariffAsync(Guid tariffId)
    {
        var tariff = await _dataService.Tariffs.GetByIdAsync(tariffId);
        if (tariff == null) return false;   

        await _dataService.Tariffs.UpdateAsync(tariff);
        return true;
    }

    public async Task<bool> DeleteTariffAsync(Guid tariffId)
    {
        try
        {
            await _dataService.Tariffs.DeleteAsync(tariffId);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<IEnumerable<TariffDto>> GetAllTariffsAsync()
    {
        var tariffs = await _dataService.Tariffs.GetAllAsync();
        return tariffs;
    }
}

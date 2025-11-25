using HostMarket.Core.Repositories;
using HostMarket.Core.Services.Interfaces;
using HostMarket.Infrastructure.Data;
using HostMarket.Infrastructure.Data.DTO;
using HostMarket.Shared.Dto;
using HostMarket.Shared.DTO;
using HostMarket.Shared.Models;
using System;
using System.Data.Common;
using static HostMarket.Core.Services.Interfaces.IAdminBFFService;

namespace HostMarket.Core.Services.Implementations.Bff;

public class AdminBFFService : IAdminBFFService
{
    private readonly IDataService _dataService;

    public AdminBFFService(IDataService dataService)
    {
        _dataService = dataService;
    }

    // Creating Server function
    public async Task<AdminResult> CreateServerAsync(CreateServerDTO createDTO)
    {
        // Creating a new ServerDto
        var selectedTariff = await _dataService.Tariffs.GetByIdAsync(createDTO.TariffId) ?? throw new Exception("The tariff was not found.");
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
        await _dataService.Servers.CreateAsync(server);

        var dict = new Dictionary<string, string>();
        dict.Add("IP", "1111");

        return new AdminResult
        {
            Success = true,
            Data = dict
        };

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

    public async Task<AdminResult> CreateTariffAsync(CreateTariffDto createTariffDto)
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

        await _dataService.Tariffs.CreateAsync(tariff);
        return new AdminResult { Success = true, Message = "The tariff has been created." };
    }

    public async Task<AdminResult> UpdateTariffAsync(Guid tariffId)
    {
        var tariff = await _dataService.Tariffs.GetByIdAsync(tariffId);
        if (tariff == null)
            return new AdminResult { Success = false, Message = "The tariff was not found." };

        await _dataService.Tariffs.UpdateAsync(tariff);
        return new AdminResult { Success = true, Message = "The tariff has been updated." };
    }

    public async Task<AdminResult> DeleteTariffAsync(Guid tariffId)
    {
        try
        {
            var result = await _dataService.Tariffs.DeleteAsync(tariffId);
            return new AdminResult { Success = result, Message = "The tariff has been deleted." };
        }
        catch
        {
            return new AdminResult { Success = false, Message = "Error. The tariff was not deleted." };
        }
    }

    public async Task<IEnumerable<TariffDto>> GetAllTariffsAsync()
    {
        var tariffs = await _dataService.Tariffs.GetAllAsync();
        return tariffs;
    }
}

using HostMarket.Core.Repositories;
using HostMarket.Core.Services.Interfaces;
using HostMarket.Infrastructure.Data;
using HostMarket.Infrastructure.Data.DTO;
using HostMarket.Shared.Dto;
using HostMarket.Shared.DTO;
using HostMarket.Shared.Models;
using System;
using System.Data.Common;
using Docker.DotNet; 

namespace HostMarket.Core.Services.Implementations.Bff;

public class AdminBFFService : IAdminBFFService
{
    private readonly IDataService _dataService;
    
    private readonly DockerClient _dockerClient;

    public AdminBFFService(IDataService dataService)
    {
        _dataService = dataService;
        _dockerClient = new DockerClientConfiguration(
            new Uri("npipe://./pipe/docker_engine") // Windows
        ).CreateClient();
    }

    public async Task<AdminResult<object>> CreateServerAsync(CreateServerDTO createDTO)
    {
        var selectedTariff = await _dataService.Tariffs.GetByIdAsync(createDTO.TariffId)
            ?? throw new Exception("The tariff was not found.");
        var serverId = Guid.NewGuid();

        var container = await _dockerClient.Containers.CreateContainerAsync(
            new Docker.DotNet.Models.CreateContainerParameters
            {
                Image = "nginx:latest", 
                Name = $"server_{serverId}",
                ExposedPorts = new Dictionary<string, Docker.DotNet.Models.EmptyStruct>
                {
                    { "80/tcp", default }
                },
                HostConfig = new Docker.DotNet.Models.HostConfig
                {
                    PortBindings = new Dictionary<string, IList<Docker.DotNet.Models.PortBinding>>
                    {
                        {
                            "80/tcp",
                            new List<Docker.DotNet.Models.PortBinding>
                            {
                                new Docker.DotNet.Models.PortBinding { HostPort = "" } 
                            }
                        }
                    }
                }
            }
        );

        await _dockerClient.Containers.StartContainerAsync(container.ID, null);

        var inspect = await _dockerClient.Containers.InspectContainerAsync(container.ID);
        string containerIp = inspect.NetworkSettings.Networks.First().Value.IPAddress;
        string hostPort = inspect.NetworkSettings.Ports["80/tcp"].First().HostPort;

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
            Status = Status.Active,
            IP = containerIp,
            Port = int.Parse(hostPort),
            ContainerId = container.ID
        };

        await _dataService.Servers.CreateAsync(server);

        return new AdminResult<object> 
        {
            Success = true,
            Message = $"Server created with container {container.ID} at {containerIp}:{hostPort}"
        };
    }


    public async Task<AdminResult<IEnumerable<ServerDTO>>> GetAllServersAsync()
    {
        var servers = await _dataService.Servers.GetAllAsync();

        if (servers is null)
        {
            return new AdminResult<IEnumerable<ServerDTO>>
            {
                Success = false,  
                DataList = null,
                ErrorMessage = "Servers not founds"
            }; 
        }   
        foreach (var server in servers)
        {
            if (!string.IsNullOrEmpty(server.ContainerId))
            {
                try
                {
                    var container = await _dockerClient.Containers.InspectContainerAsync(server.ContainerId);
                    server.ServStatus = container.State.Running ? ServerStatus.Running : ServerStatus.Stopped;
                    server.IP = container.NetworkSettings.Networks.First().Value.IPAddress;
                    server.Port = int.Parse(container.NetworkSettings.Ports["80/tcp"]?.FirstOrDefault()?.HostPort);
                    {
                        
                    };
                }
                catch
                {
                    server.ServStatus = ServerStatus.Unknown;
                }
            }
            else
            {
                server.ServStatus = ServerStatus.Unknown;
            }
        }

        return new AdminResult<IEnumerable<ServerDTO>>
        {
            Success = true,
            DataList = new List<IEnumerable<ServerDTO>> { servers }
        };
    }

    public async Task<AdminResult<object>> UpdateServerInfoAsync(Guid serverId)
    {
        var serverDto = await _dataService.Servers.GetByIdAsync(serverId);

        if (serverDto != null)
        {
            await _dataService.Servers.UpdateAsync(serverDto);
            return new AdminResult<object>
            {
                Success = true,
                Message = "The server info has been upgraded successfully."
            };
        }
        else
        {
            return new AdminResult<object>
            {
                Success = false,
                Message = "The server cannot be found."
            };
        }
    }


    public async Task<AdminResult<object>> DeleteServerAsync(Guid serverId)
    {
        var server = await _dataService.Servers.GetByIdAsync(serverId);
        if (server == null)
            return new AdminResult<object> { Success = false, Message = "Server not found." };

        try
        {
            await _dockerClient.Containers.StopContainerAsync(server.ContainerId,
                new Docker.DotNet.Models.ContainerStopParameters());
            await _dockerClient.Containers.RemoveContainerAsync(server.ContainerId,
                new Docker.DotNet.Models.ContainerRemoveParameters { Force = true });
        }
        catch
        {
            return new AdminResult<object> { Success = false, Message = "Failed to remove container." };
        }

        await _dataService.Servers.DeleteAsync(serverId);

        return new AdminResult<object>
        {
            Success = true,
            Message = "Server and container deleted successfully."
        };
    }


    public async Task<ServerResult> GetServerStatusAsync(Guid serverId)
    {
        var server = await _dataService.Servers.GetByIdAsync(serverId);
        if (server == null)
            throw new Exception("Server not found.");

        var container = await _dockerClient.Containers.InspectContainerAsync(server.ContainerId);

        var status = container.State.Running ? ServerStatus.Running : ServerStatus.Stopped;

        return new ServerResult
        {
            Status = status,
            Ip = server.IP,
            Port = server.Port
        };
    }

    public async Task<AdminResult<object>> CreateTariffAsync(CreateTariffDto createTariffDto)
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
        return new AdminResult<object> { Success = true, Message = "The tariff has been created." };
    }

    public async Task<AdminResult<object>> UpdateTariffAsync(Guid tariffId)
    {
        var tariff = await _dataService.Tariffs.GetByIdAsync(tariffId);
        if (tariff == null)
            return new AdminResult<object> { Success = false, Message = "The tariff was not found." };

        await _dataService.Tariffs.UpdateAsync(tariff);
        return new AdminResult<object> { Success = true, ErrorMessage = "The tariff has been updated." };
    }

    public async Task<AdminResult<object>> DeleteTariffAsync(Guid tariffId)
    {
        try
        {
            var result = await _dataService.Tariffs.DeleteAsync(tariffId);
            return new AdminResult<object> { Success = result, Message = "The tariff has been deleted." };
        }
        catch
        {
            return new AdminResult<object> { Success = false, ErrorMessage = "Error. The tariff was not deleted." };
        }
    }

    public async Task<AdminResult<IEnumerable<TariffDto>>> GetAllTariffsAsync()
    {
        var tariffs = await _dataService.Tariffs.GetAllAsync();
        return new AdminResult<IEnumerable<TariffDto>>
        {
            Success = true,
            DataList = new List<IEnumerable<TariffDto>> { tariffs }
        };
    }
}

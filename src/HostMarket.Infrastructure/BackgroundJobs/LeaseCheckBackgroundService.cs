using HostMarket.Core.Services.Implementations.Bff;
using HostMarket.Core.Services.Interfaces;
using HostMarket.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Infrastructure.BackgroundJobs
{
    public class LeaseCheckBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public LeaseCheckBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var dataService = scope.ServiceProvider.GetRequiredService<IDataService>();
                var serverBffService = scope.ServiceProvider.GetRequiredService<IServerBFFService>();
                var servers = await dataService.Servers.GetServersWithCompletedLeasesAsync();

                foreach (var server in servers)
                {
                    try
                    {
                        // attempt to extend the lease
                        await serverBffService.ServerRentalAsync((Guid)server.ownerId!, server.Id);
                    }
                    catch (Exception ex)
                    {
                        if (await serverBffService.ResetLease(server))
                        {
                            // replace it with a logger in the future
                            Console.WriteLine($"Failed to extend lease for user {server.ownerId}, server {server.Id}");
                        }
                        else Console.WriteLine(ex);
                    }
                }
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}


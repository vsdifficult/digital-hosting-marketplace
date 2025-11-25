using HostMarket.Core.Services.Interfaces;
using HostMarket.Infrastructure.Data.DTO;
using HostMarket.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HostMarket.Core.Services.Interfaces.IAdminBFFService;

namespace HostMarket.Core.Services.Implementations.Bff
{
    public class ServerBFFService : IServerBFFService
    {
        private readonly IDataService _dateService;
        private readonly IAdminBFFService _adminBFFService;
        public ServerBFFService(IDataService dataService, IAdminBFFService adminBFFService)
        {
            _dateService = dataService;
            _adminBFFService = adminBFFService;
        }
        public async Task<ServerResult> ServerRentalAsync(Guid userId, Guid serverId)
        {
            var user = await _dateService.Users.GetByIdAsync(userId);
            if (user == null ||
                user.Status == Shared.Models.Status.Deleted) throw new Exception("The user was not found.");

            var server = await _dateService.Servers.GetByIdAsync(serverId);
            if (server == null ||
                server.ServStatus == Shared.Models.ServerStatus.Purchased ||
                server.Status == Shared.Models.Status.Deleted) throw new Exception("The server is not available for rent.");

            if (user.Balance >= server.Price)
            {
                server.ownerId = userId;
                user.Balance -= server.Price;
                server.ServStatus = Shared.Models.ServerStatus.Purchased;
                server.UpdateAt = DateTime.UtcNow;
                user.UpdateAt = DateTime.UtcNow;
                
                return new ServerResult 
                {
                    Ip = "ip",
                    Port = 1111
                };
            }

            else throw new Exception("Insufficient funds to pay the rent.");

        }
    }

}

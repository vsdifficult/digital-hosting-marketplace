using HostMarket.Core.Services.Interfaces;
using HostMarket.Infrastructure.Data.DTO;
using HostMarket.Shared.Dto;
using HostMarket.Shared.Models;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Core.Services.Implementations.Bff
{
    public class ServerBFFService : IServerBFFService
    {
        private readonly IDataService _dataService;
        public ServerBFFService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<bool> ConfirmTransactionAsync(Guid transactionId)
        {
            // Error checking
            try
            {
                // Getting the transaction
                var transaction = await _dataService.Transactions.GetByIdAsync(transactionId);

                // If Transaction==null -> throw Exception
                if (transaction == null) throw new Exception("Transaction cannot be found.");

                // trStatus
                var transactionStatus = transaction.transactionStatus;

                if (transactionStatus == TransactionStatus.Finished)
                    return true;
                else if (transactionStatus == TransactionStatus.Pending)
                {
                    transaction.transactionStatus = TransactionStatus.Finished;
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        private async Task<Guid> MakeTransactionAsync(Guid userId, decimal amount, Guid serverId)
        {
            TransactionDto dto = new TransactionDto
            {
                userId = userId,
                serverId = serverId,
                Amount = amount,
                transactionStatus = TransactionStatus.Pending
            };
            return await _dataService.Transactions.CreateAsync(dto);
        }

        private async Task<ServerResult> ExtendLease(UserDTO user, ServerDTO server)
        {
            if (user.Balance >= server.Price)
            {
                user.Balance -= server.Price;
                server.RentalEnd = DateTime.UtcNow.AddMonths(1);

                if (await ConfirmTransactionAsync(await MakeTransactionAsync(user.Id, server.Price, server.Id))
                    && await _dataService.Users.UpdateAsync(user)
                    && await _dataService.Servers.UpdateAsync(server))
                {
                    return new ServerResult
                    {
                        Ip = "ip",
                        Port = 1111,
                        Status = server.ServStatus
                    };
                }
                else return new ServerResult { ErrorMessage = "Couldn't extend lease." };
            }
            else return new ServerResult { ErrorMessage = "Insufficient funds to pay the rent." };
        }
        public async Task<ServerResult> ServerRentalAsync(Guid userId, Guid serverId)
        {
            var user = await _dataService.Users.GetByIdAsync(userId);
            if (user == null ||
                user.Status == Shared.Models.Status.Deleted) throw new Exception("The user was not found.");

            var server = await _dataService.Servers.GetByIdAsync(serverId);

            if (server?.ownerId == userId)
            {
                return await ExtendLease(user, server);
            }

            if (server == null ||
                server.ServStatus == Shared.Models.ServerStatus.Purchased ||
                server.Status == Shared.Models.Status.Deleted) throw new Exception("The server is not available for rent.");

            if (user.Balance >= server.Price)
            {
                server.ownerId = userId;
                user.Balance -= server.Price;
                server.ServStatus = ServerStatus.Purchased;
                server.RentalStart = DateTime.UtcNow;
                server.RentalEnd = DateTime.UtcNow.AddMonths(1);

                if (await ConfirmTransactionAsync(await MakeTransactionAsync(userId, server.Price, serverId))
                    && await _dataService.Users.UpdateAsync(user)
                    && await _dataService.Servers.UpdateAsync(server))
                {
                    return new ServerResult
                    {
                        Ip = "ip",
                        Port = 1111,
                        Status = server.ServStatus
                    };
                }
                else return new ServerResult { ErrorMessage = "Error. Transaction not confirmed." };
            }
            else return new ServerResult { ErrorMessage = "Insufficient funds to pay the rent." };
        }

        public async Task<bool> ResetLease(ServerDTO server)
        {
            try
            {
                server.ownerId = null;
                server.ServStatus = Shared.Models.ServerStatus.Available;
                server.RentalStart = null;
                server.RentalEnd = null;
                server.UpdateAt = DateTime.UtcNow;

                return await _dataService.Servers.UpdateAsync(server);
            }
            catch
            {
                return false;
            }
        }
    }

    // Health check
    //public async Task<ServerResult> HealthCheckAsync(Guid serverId)
    //{
    //    var server = await _dataService.Servers.GetByIdAsync(serverId);

    //    // // if server==null -> throw Exceprion
    //    if (server == null) throw new Exception("Server cannot be found.");

    //    // try to ping the server
    //    var server_address = server.Address;

    //    try
    //    {
    //        // if the server was found, we return the status
    //        Ping ping = new Ping();
    //        PingReply pingReply = ping.Send(server_address, 1000);  // Time-out for a 1000 second

    //        // Check for the server request        
    //        if (pingReply.Status == IPStatus.Success)
    //        {
    //            return new ServerResult
    //            {
    //                Status = ServerStatus.Available
    //            };
    //        }
    //        else
    //        {
    //            return new ServerResult
    //            {
    //                Status = ServerStatus.Purchased
    //            };
    //        }
    //    }

    //    catch
    //    {
    //        return new ServerResult
    //        {
    //            ErrorMessage = "Server cannot be Ping"
    //        };
    //    }
    //}

}



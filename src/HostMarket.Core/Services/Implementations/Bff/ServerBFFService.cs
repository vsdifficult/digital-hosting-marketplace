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
        private readonly IDataService _dataService;
        public ServerBFFService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<AdminResult> ConfirmTransactionAsync(Guid transactionId)
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
                {
                    return new AdminResult
                    {
                        Success = true,
                        transactionStatus = TransactionStatus.Finished
                    };
                }

                else if (transactionStatus == TransactionStatus.Pending)
                {
                    return new AdminResult
                    {
                        Success = true,
                        transactionStatus = TransactionStatus.Pending
                    };
                }

                else
                {
                    return new AdminResult
                    {
                        Success = false,
                        transactionStatus = TransactionStatus.Denied
                    };
                }
            }


            catch
            {
                return new AdminResult
                {
                    Success = false,
                    transactionStatus = TransactionStatus.Error,
                    ErrorMessage = "Transaction Error."
                };
            }
        }

        public async Task<Guid> MakeTransactionAsync(Guid userId, decimal amount, Guid serverId)
        {
            TransactionDto dto = new TransactionDto
            {
                userId = userId,
                serverId = serverId,
                Amount = amount
            };
            return await _dataService.Transactions.CreateAsync(dto);
        }

        public async Task<ServerResult> ServerRentalAsync(Guid userId, Guid serverId)
        {
            var user = await _dataService.Users.GetByIdAsync(userId);
            if (user == null ||
                user.Status == Shared.Models.Status.Deleted) throw new Exception("The user was not found.");

            var server = await _dataService.Servers.GetByIdAsync(serverId);
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

                var transactionId = await MakeTransactionAsync(userId, server.Price, serverId);
                //return await ConfirmTransactionAsync(transactionId);
                
                return new ServerResult 
                {
                    Ip = "ip",
                    Port = 1111,
                    Status = server.ServStatus
                };
            }

            else throw new Exception("Insufficient funds to pay the rent.");

        }
    }

}

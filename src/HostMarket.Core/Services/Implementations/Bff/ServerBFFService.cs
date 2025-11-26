using HostMarket.Core.Services.Interfaces;
using HostMarket.Shared.Dto;
using HostMarket.Infrastructure.Data;
using HostMarket.Infrastructure.Data.DTO;
using HostMarket.Shared.Models;
using HostMarket.Core.Repositories;
using System.Data.Common;
using System.Net.NetworkInformation;
using System.Diagnostics;

namespace HostMarket.Core.Services.Implementations.Bff;


public class ServerBFFService : IServerBFFService
{
    private readonly IDataService _dataService; 

    public ServerBFFService(IDataService dataService)
    {
        _dataService = dataService; 
    }


    // Health check
    public async Task<ServerResult> HealthCheckAsync(Guid serverId)
    {
        var server = await _dataService.Servers.GetByIdAsync(serverId);

        // // if server==null -> throw Exceprion
        if (server == null) throw new Exception("Server cannot be found.");

        // try to ping the server
        var server_address = server.Address;

        try
        {
        // if the server was found, we return the status
        Ping ping = new Ping();
        PingReply pingReply = ping.Send(server_address, 1000);  // Time-out for a 1000 second

        // Check for the server request        
        if (pingReply.Status == IPStatus.Success)
        {
            return new ServerResult{
                Status = ServerStatus.Available  
                };
        }
        else
        {
            return new ServerResult{
                Status = ServerStatus.Purchased
                };
        }
        }

        catch
        {
            return new ServerResult{
                ErrorMessage = "Server cannot be Ping"  
                };
        }
    }
    

    // Server Rent
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
            
            var ifConfirm = ConfirmTransactionAsync(transactionId);


            return new ServerResult 
            {
                Ip = "ip",
                Port = 1111
            };
        }

        else throw new Exception("Insufficient funds to pay the rent.");

    }


    public async Task<Guid> MakeTransactionAsync(Guid userId, decimal amount, Guid serverId)
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


    // ConfirmTransaction
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
                return new AdminResult {
                    Success = true, 
                    transactionStatus = TransactionStatus.Finished
                    };
            }

            else if (transactionStatus == TransactionStatus.Pending)
            {
                return new AdminResult {
                Success = true,
                transactionStatus = TransactionStatus.Pending    
                };
            }

            else
            {
                return new AdminResult {
                    Success = false,
                    transactionStatus = TransactionStatus.Denied
                };
            }            
        }


        catch
        {
            return new AdminResult {
                Success = false,
                transactionStatus = TransactionStatus.Error, 
                ErrorMessage = "Transaction Error."
                };
        }
    }




}

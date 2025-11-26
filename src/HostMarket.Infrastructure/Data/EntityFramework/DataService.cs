

using HostMarket.Core.Repositories;
using HostMarket.Core.Services.Interfaces;
using HostMarket.Infrastructure.Data.EntityFramework.Repositories; 

namespace HostMarket.Infrastructure.Data.EntityFramework; 

public class DataService: IDataService
{
    private readonly DataContext _context;

    public DataService(DataContext context)
    {
        _context = context;

        Users = new UserRepository(_context);
        Servers = new ServerRepository(_context); 
        Transactions = new TransactionRepository(_context);
        Tariffs = new TariffRepository(_context);
    } 
    
    public IUserRepository Users { get; }

    public IServerRepository Servers { get; }

    public ITransactionRepository Transactions { get; }
    public ITariffRepository Tariffs { get; }
}
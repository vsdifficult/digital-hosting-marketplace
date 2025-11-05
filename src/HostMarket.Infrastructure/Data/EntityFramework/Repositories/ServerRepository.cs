

using HostMarket.Core.Repositories;

namespace HostMarket.Infrastructure.Data.EntityFramework; 

public class ServerRepository: IServerRepository
{
    private readonly DataContext _context;
    public ServerRepository(DataContext context)
    {
        _context = context; 
    } 
}
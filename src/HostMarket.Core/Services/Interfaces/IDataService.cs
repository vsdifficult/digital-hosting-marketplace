using HostMarket.Core.Repositories; 

namespace HostMarket.Core.Services.Interfaces;

public interface IDataService
{
    IUserRepository Users { get; }  
    IServerRepository Servers { get; }
}
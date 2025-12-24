using HostMarket.Infrastructure.Data.DTO;
using HostMarket.Shared.Dto;
using HostMarket.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Core.Repositories
{
    public interface IServerRepository : IRepository<ServerDTO, Guid>
    {
        Task<IEnumerable<ServerDTO>> GetServersWithCompletedLeasesAsync();
    }
}



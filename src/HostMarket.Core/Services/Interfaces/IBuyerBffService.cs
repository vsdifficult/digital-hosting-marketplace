using HostMarket.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Core.Services.Interfaces
{
    public interface IBuyerBffService
    {
        Task<UserDTO?> GetUserContextAsync(Guid userId);
        Task<decimal?> GetUserBalanceAsync(Guid userId);
    }
}

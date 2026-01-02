using HostMarket.Core.Services.Interfaces;
using HostMarket.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Core.Services.Implementations.Bff
{
    public class BuyerBffService : IBuyerBffService
    {
        private readonly IDataService _dataService;
        public BuyerBffService (IDataService dataService)
        {
            _dataService = dataService;
        }
        public async Task<decimal?> GetUserBalanceAsync(Guid userId)
        {
            var user = await _dataService.Users.GetByIdAsync(userId);
            if (user == null) return null;

            return user.Balance;
        }

        public async Task<UserDTO?> GetUserContextAsync(Guid userId)
        {
            var user = await _dataService.Users.GetByIdAsync(userId);
            if (user == null) return null;

            return user;
        }
    }
}

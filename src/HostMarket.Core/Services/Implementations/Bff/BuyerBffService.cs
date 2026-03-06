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
        public async Task<BuyerResult<object>> GetUserBalanceAsync(Guid userId)
        {
            var user = await _dataService.Users.GetByIdAsync(userId);
            if (user == null)
            {
                return new BuyerResult<object>
                {
                    Success = false,
                    ErrorMessage = "Error. The user was not found."
                };
            }

            return new BuyerResult<object>
            {
                Success = true,
                Balance = user.Balance
            };
        }

        public async Task<BuyerResult<object>> GetUserContextAsync(Guid userId)
        {
            var user = await _dataService.Users.GetByIdAsync(userId);
            if (user == null)
            {
                return new BuyerResult<object>
                {
                    Success = false,
                    ErrorMessage = "Error. The user was not found."
                };
            }

            return new BuyerResult<object>
            {
                Success = true,
                User = user
            };
        }
    }
}

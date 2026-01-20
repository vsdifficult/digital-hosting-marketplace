using HostMarket.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Core.Services.Interfaces
{
    public record BuyerResult<T>
    {
        public bool Success { get; init; }
        public decimal Balance { get; init; }
        public string? Message { get; init; }
        public string? ErrorMessage { get; init; }
        public UserDTO? User { get; init; }
    }
    public interface IBuyerBffService
    {
        Task<BuyerResult<object>> GetUserContextAsync(Guid userId);
        Task<BuyerResult<object>> GetUserBalanceAsync(Guid userId);
    }
}

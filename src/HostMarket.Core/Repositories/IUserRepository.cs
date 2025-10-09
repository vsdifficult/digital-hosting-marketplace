using HostMarket.Infrastructure.Data.DTO;
using HostMarket.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Core.Repositories
{
    public interface IUserRepository : IRepository<UserDTO, Guid>
    {
        Task<bool> UpdatePasswordAsync(Guid userid, string new_password);
        Task<UserDTO?> GetByEmailAsync(string email);
        Task<UserDTO?> GetByUsernameAsync(string username);
        Task<List<UserDTO>> GetUnverifiedOlderThanAsync(DateTime cutoff);
        Task<UserRole> GetUserRoleAsync(Guid userId);
        Task<bool> IsUserExistsByEmailAsync(string email);
        Task<IEnumerable<UserDTO>> GetUsersByRoleAsync(UserRole role);

        Task<bool> SetEmailVerifiedAsync(string email);
        Task<bool> SetVerificationCodeAsync(string email, string code);
        Task<int> GetUserCountAsync();
    }
}
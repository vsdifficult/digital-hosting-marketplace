using HostMarket.Core.Repositories;
using HostMarket.Shared.Dto;
using HostMarket.Infrastructure.Data.Entities;
using HostMarket.Infrastructure.Data.EntityFramework.Mappers;
using HostMarket.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HostMarket.Infrastructure.Data.EntityFramework.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAsync(UserDTO entity)
        {
            var userEntity = UserMapper.FromUserDTOToEntity(entity);
            userEntity.Id = Guid.NewGuid();

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return userEntity.Id;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            return await _context.Users
                .Select(user => UserMapper.FromEntityToDto(user))
                .ToListAsync();
        }

        public async Task<UserDTO?> GetByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user == null ? null : UserMapper.FromEntityToDto(user);
        }

        public async Task<UserDTO?> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            return user == null ? null : UserMapper.FromEntityToDto(user);
        }

        public async Task<UserDTO?> GetByUsernameAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            return user == null ? null : UserMapper.FromEntityToDto(user);
        }

        public async Task<List<UserDTO>> GetUnverifiedOlderThanAsync(DateTime cutoff)
        {
            return await _context.Users
              .Where(u => !u.IsVerify && u.CreateAt < cutoff)
              .Select(u => UserMapper.FromEntityToDto(u))
              .ToListAsync();
        }

        public async Task<int> GetUserCountAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<UserRole> GetUserRoleAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user?.Role ?? UserRole.User;
        }

        public async Task<IEnumerable<UserDTO>> GetUsersByRoleAsync(UserRole role)
        {
            return await _context.Users
               .Where(u => u.Role == role)
               .Select(u => UserMapper.FromEntityToDto(u))
               .ToListAsync();
        }

        public async Task<bool> IsUserExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> SetEmailVerifiedAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return false;

            user.IsVerify = true;
            user.UpdateAt = DateTime.UtcNow;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> SetVerificationCodeAsync(string email, string code)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return false;

            user.Code = code;
            user.UpdateAt = DateTime.UtcNow;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(UserDTO entity)
        {
            var user = await _context.Users.FindAsync(entity.Id);
            if (user == null) return false;

            UserMapper.UpdateEntity(user, entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePasswordAsync(Guid userid, string new_password)
        {
            var user = await _context.Users.FindAsync(userid);
            if (user == null) return false;

            user.Password = new_password;
            user.UpdateAt = DateTime.UtcNow;

            return await _context.SaveChangesAsync() > 0;
        }

        // Доделать маперы
        private UserDTO MapToDto(UserEntity entity)
        {
            return new UserDTO
            {
                Id = entity.Id,
                Email = entity.Email,
                UserName = entity.UserName,
                Password = entity.Password,
                Balance = entity.Balance,
                IsVerify = entity.IsVerify,
                RegistrationDate = entity.RegistrationDate
            };
        }

        private UserEntity MapToEntity(UserDTO dto)
        {
            return new UserEntity
            {
                Id = dto.Id,
                Email = dto.Email,
                UserName = dto.UserName,
                Password = dto.Password,
                Balance = dto.Balance,
                IsVerify = dto.IsVerify,
                RegistrationDate = dto.RegistrationDate
            };
        }
        
    }
}
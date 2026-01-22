using HostMarket.Shared.Dto;
using HostMarket.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Infrastructure.Data.EntityFramework.Mappers
{
    public static class UserMapper
    {
        public static UserDTO FromEntityToDto(UserEntity entity)
        {
            return new UserDTO
            {
                Id = entity.Id,
                Email = entity.Email,
                UserName = entity.UserName,
                Role = entity.Role,
                Password = entity.Password,
                Code = entity.Code,
                IsVerify  = entity.IsVerify,
                Balance = entity.Balance,
                RegistrationDate = entity.RegistrationDate,
                CreateAt = entity.CreateAt,
                UpdateAt = entity.UpdateAt
            };
        }
        public static UserEntity FromUserDTOToEntity(UserDTO dto)
        {
            return new UserEntity
            {
                Id = dto.Id,
                Email = dto.Email,
                UserName = dto.UserName,
                Role = dto.Role,
                Password = dto.Password,
                Code = dto.Code,
                IsVerify = dto.IsVerify,
                Balance = dto.Balance,
                RegistrationDate = dto.RegistrationDate,
                CreateAt = dto.CreateAt,
                UpdateAt = dto.UpdateAt
            };
        }

        public static void UpdateEntity(UserEntity entity, UserDTO dto)
        {
            entity.Email = dto.Email;
            entity.UserName = dto.UserName;
            entity.Role = dto.Role;
            entity.Password = dto.Password;
            entity.Balance = dto.Balance;
            entity.Code = dto.Code;
            entity.IsVerify = dto.IsVerify;
            entity.RegistrationDate = dto.RegistrationDate;
            entity.CreateAt = dto.CreateAt;
            entity.UpdateAt = DateTime.UtcNow;
        }
    }
}

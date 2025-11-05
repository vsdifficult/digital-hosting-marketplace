using HostMarket.Infrastructure.Data.DTO;
using HostMarket.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Infrastructure.Data.EntityFramework.Mappers
{
    public class ServerMapper
    {
        public static ServerDTO FromEntityToDto(ServerEntity entity)
        {
            return new ServerDTO
            {
                Id = entity.Id,
                ownerId = entity.ownerId,
                ServerName = entity.ServerName,
                Description = entity.Description,
                Price = entity.Price,
                ServStatus = entity.ServStatus,
                CreateAt = entity.CreateAt,
                UpdateAt = entity.UpdateAt,
                Status = entity.Status
            };
        }

        public static ServerEntity FromDtoToEntity(ServerDTO dto)
        {
            return new ServerEntity
            {
                Id = dto.Id,
                ownerId = dto.ownerId,
                ServerName = dto.ServerName,
                Description = dto.Description,
                Price = dto.Price,
                ServStatus = dto.ServStatus,
                CreateAt= dto.CreateAt,
                UpdateAt= dto.UpdateAt,
                Status = dto.Status
            };
        }

        public static void Update(ServerEntity entity, ServerDTO dto)
        {
            entity.ownerId = dto.ownerId;
            entity.ServerName = dto.ServerName;
            entity.Description = dto.Description;
            entity.Price = dto.Price;
            entity.ServStatus = dto.ServStatus;
            entity.CreateAt = dto.CreateAt;
            entity.UpdateAt = DateTime.UtcNow;
            entity.Status = dto.Status;
        }
    }
}

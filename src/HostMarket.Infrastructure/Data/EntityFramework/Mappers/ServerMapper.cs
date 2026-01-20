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
        public static ServerDTO FromEntityToDto(ServerEntity body)
        {
            return new ServerDTO
            {
                Id = body.Id,
                ownerId = body.ownerId,
                TariffId = body.TariffId,
                ServerName = body.ServerName,
                Description = body.Description,
                Price = body.Price,
                ServStatus = body.ServStatus,
                CreateAt = body.CreateAt,
                UpdateAt = body.UpdateAt,
                Status = body.Status,
                RentalStart = body.RentalStart,
                RentalEnd = body.RentalEnd,
                IP = body.IP,
                Port = body.Port,
                ContainerId = body.ContainerId
            };
        }

        public static ServerEntity FromDtoToEntity(ServerDTO body)
        {
            return new ServerEntity
            {
                Id = body.Id,
                ownerId = body.ownerId,
                TariffId = body.TariffId,
                ServerName = body.ServerName,
                Description = body.Description,
                Price = body.Price,
                ServStatus = body.ServStatus,
                CreateAt = body.CreateAt,
                UpdateAt = body.UpdateAt,
                Status = body.Status,
                RentalStart = body.RentalStart,
                RentalEnd = body.RentalEnd,
                IP = body.IP,
                Port = body.Port,
                ContainerId = body.ContainerId
            };
        }

        public static void Update(ServerEntity entity, ServerDTO dto)
        {
            entity.ownerId = dto.ownerId;
            entity.TariffId = dto.TariffId;
            entity.ServerName = dto.ServerName;
            entity.Description = dto.Description;
            entity.Price = dto.Price;
            entity.ServStatus = dto.ServStatus;
            entity.CreateAt = dto.CreateAt;
            entity.UpdateAt = DateTime.UtcNow;
            entity.Status = dto.Status;
            entity.RentalStart = dto.RentalStart;
            entity.RentalEnd = dto.RentalEnd; 
            entity.IP = dto.IP; 
            entity.Port = dto.Port;
            entity.ContainerId = dto.ContainerId;
        }
    }
}

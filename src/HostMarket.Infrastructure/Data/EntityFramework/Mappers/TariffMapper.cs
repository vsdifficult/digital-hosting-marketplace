using HostMarket.Infrastructure.Data.Entities;
using HostMarket.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Infrastructure.Data.EntityFramework.Mappers
{
    public class TariffMapper
    {
        public static TariffDto FromEntityToDto (TariffEntity entity)
        {
            return new TariffDto
            {
                Id = entity.Id,
                TariffName = entity.TariffName,
                RamGb = entity.RamGb,
                DiskGb = entity.DiskGb,
                Price = entity.Price,
                Status = entity.Status,
                CreateAt = entity.CreateAt,
                UpdateAt = entity.UpdateAt
            };
        }
        public static TariffEntity FromDtoToEntity (TariffDto dto)
        {
            return new TariffEntity
            {
                Id = dto.Id,
                TariffName = dto.TariffName,
                RamGb = dto.RamGb,
                DiskGb = dto.DiskGb,
                Price = dto.Price,
                Status = dto.Status,
                CreateAt = dto.CreateAt,
                UpdateAt = dto.UpdateAt
            };
        }
        public static void Update (TariffEntity entity, TariffDto dto)
        {
            entity.TariffName = dto.TariffName;
            entity.RamGb = dto.RamGb;
            entity.DiskGb = dto.DiskGb;
            entity.Price = dto.Price;
            entity.Status = dto.Status;
            entity.CreateAt = dto.CreateAt;
            entity.UpdateAt = DateTime.UtcNow;
        }
    }
}

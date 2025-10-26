using HostMarket.Infrastructure.Data.Entities;
using HostMarket.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Infrastructure.Data.EntityFramework.Mappers
{
    public class TransactionMapper
    {
        public static TransactionDto FromEntityToDto(TransactionEntity entity)
        {
            return new TransactionDto
            {
                Id = entity.Id,
                userId = entity.userId,
                serverId = entity.serverId,
                Amount = entity.Amount,
                transactionDate = entity.transactionDate,
                CreateAt = entity.CreateAt,
                UpdateAt = entity.UpdateAt,
                Status = entity.Status
            };
        }
        public static TransactionEntity FromDtoToEnity(TransactionDto dto)
        {
            return new TransactionEntity
            {
                Id = dto.Id,
                userId = dto.userId,
                serverId = dto.serverId,
                Amount = dto.Amount,
                transactionDate = dto.transactionDate,
                CreateAt = dto.CreateAt,
                UpdateAt = dto.UpdateAt,
                Status = dto.Status

            };
        }
        public static void Update(TransactionEntity entity, TransactionDto dto)
        {
            entity.userId = dto.userId;
            entity.serverId = dto.serverId;
            entity.Amount = dto.Amount;
            entity.transactionDate = dto.transactionDate;
            entity.UpdateAt = DateTime.UtcNow;
            entity.Status = dto.Status;
        }
    }
}

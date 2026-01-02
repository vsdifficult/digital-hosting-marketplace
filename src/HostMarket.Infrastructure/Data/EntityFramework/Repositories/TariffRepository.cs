using HostMarket.Core.Repositories;
using HostMarket.Infrastructure.Data.EntityFramework.Mappers;
using HostMarket.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Infrastructure.Data.EntityFramework.Repositories
{
    public class TariffRepository : ITariffRepository
    {
        private readonly DataContext _dataContext;
        public TariffRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Guid> CreateAsync(TariffDto dto)
        {
            dto.CreateAt = DateTime.UtcNow;
            dto.UpdateAt = DateTime.UtcNow;

            var tariff = TariffMapper.FromDtoToEntity(dto);

            await _dataContext.Tariffs.AddAsync(tariff);
            await _dataContext.SaveChangesAsync();

            return tariff.Id;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var tariff = await _dataContext.Tariffs.FindAsync(id);
            if(tariff == null) return false;

            _dataContext.Tariffs.Remove(tariff);
            await _dataContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<TariffDto>> GetAllAsync()
        {
            return await _dataContext.Tariffs
            .Select(tariff => TariffMapper.FromEntityToDto(tariff))
            .ToListAsync();
        }

        public async Task<TariffDto?> GetByIdAsync(Guid id)
        {
            var tariff = await _dataContext.Tariffs.FindAsync(id);
            return tariff == null ? null : TariffMapper.FromEntityToDto(tariff);
        }

        public async Task<bool> UpdateAsync(TariffDto entity)
        {
            var tariff = await _dataContext.Tariffs.FindAsync(entity.Id);
            if (tariff == null) return false;

            TariffMapper.Update(tariff, entity);
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}

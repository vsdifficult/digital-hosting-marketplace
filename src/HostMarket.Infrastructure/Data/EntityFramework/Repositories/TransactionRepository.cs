using HostMarket.Core.Repositories;
using HostMarket.Infrastructure.Data.EntityFramework.Mappers;
using HostMarket.Shared.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostMarket.Infrastructure.Data.EntityFramework.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DataContext _context;
        public TransactionRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Guid> CreateAsync(TransactionDto entity)
        {
            var transactionEntity = TransactionMapper.FromDtoToEnity(entity);
            transactionEntity.Id = Guid.NewGuid();
            transactionEntity.CreateAt = DateTime.UtcNow;
            transactionEntity.transactionDate = DateTime.UtcNow;

            await _context.Transactions.AddAsync(transactionEntity);
            await _context.SaveChangesAsync();

            return transactionEntity.Id;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null) return false;
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<TransactionDto>> GetAllAsync()
        {
            return await _context.Transactions.Select(t => TransactionMapper.FromEntityToDto(t)).ToListAsync();
        }

        public async Task<TransactionDto?> GetByIdAsync(Guid id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null) return null;
            return TransactionMapper.FromEntityToDto(transaction);
        }

        public async Task<bool> UpdateAsync(TransactionDto entity)
        {
            var transaction = await _context.Transactions.FindAsync(entity.Id);
            if (transaction == null) return false;
            TransactionMapper.Update(transaction, entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

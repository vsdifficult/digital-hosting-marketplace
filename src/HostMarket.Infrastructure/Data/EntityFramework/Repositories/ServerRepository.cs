

using HostMarket.Core.Repositories;
using HostMarket.Infrastructure.Data.DTO;
using HostMarket.Infrastructure.Data.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;

namespace HostMarket.Infrastructure.Data.EntityFramework; 

public class ServerRepository: IServerRepository
{
    private readonly DataContext _context;
    public ServerRepository(DataContext context)
    {
        _context = context; 
    }

    public async Task<Guid> CreateAsync(ServerDTO entity)
    {
        entity.CreateAt = DateTime.UtcNow;
        entity.UpdateAt = DateTime.UtcNow;

        var serverEntity = ServerMapper.FromDtoToEntity(entity);

        await _context.Servers.AddAsync(serverEntity);
        await _context.SaveChangesAsync();

        return serverEntity.Id;

    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var server = await _context.Servers.FindAsync(id);
        if (server == null) return false;

        _context.Servers.Remove(server);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<ServerDTO>> GetAllAsync()
    {
        return  await _context.Servers
            .Select(server => ServerMapper.FromEntityToDto(server))
            .ToListAsync();
    }

    public async Task<ServerDTO?> GetByIdAsync(Guid id)
    {
        var server = await _context.Servers.FindAsync(id);
        return server == null ? null : ServerMapper.FromEntityToDto(server);
    }

    public async Task<IEnumerable<ServerDTO>> GetServersWithCompletedLeasesAsync()
    {
        var servers = await _context.Servers
           .Where(s => s.RentalEnd <= DateTime.UtcNow)
           .ToListAsync();

        return servers.Select(s => ServerMapper.FromEntityToDto(s));
    }
    public async Task<bool> UpdateAsync(ServerDTO entity)
    {
        var server = await _context.Servers.FindAsync(entity.Id);
        if (server == null ) return false;  

        ServerMapper.Update(server, entity);
        return await _context.SaveChangesAsync() > 0;
    }
}
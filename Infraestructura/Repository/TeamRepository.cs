using Core.Entities;
using Core.Interfaces;
using Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repository;

public class TeamRepository : GenericRepository<Team>, ITeam
{
    private readonly DbfirstContext _context;

    public TeamRepository(DbfirstContext context)
        : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Team>> GetAllAsync()
    {
        return await _context.Teams.Include(p => p.IdDrivers).ToListAsync();
    }

    public override async Task<Team> GetByIdAsync(int id)
    {
        return await _context.Teams.Include(p => p.IdDrivers).FirstOrDefaultAsync(p => p.Id == id);
    }
}

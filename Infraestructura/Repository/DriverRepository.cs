using Core.Entities;
using Core.Interfaces;
using Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repository;

public class DriverRepository : GenericRepository<Driver>, IDriver
{
    private readonly DbfirstContext _context;

    public DriverRepository(DbfirstContext context)
        : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Driver>> GetAllAsync()
    {
        return await _context.Drivers.Include(p => p.IdTeams).ToListAsync();
    }

    public override async Task<Driver> GetByIdAsync(int id)
    {
        return await _context.Drivers.Include(p => p.IdTeams).FirstOrDefaultAsync(p => p.Id == id);
    }
}

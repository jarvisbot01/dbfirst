using Core.Interfaces;
using Infraestructura.Data;
using Infraestructura.Repository;

namespace Infraestructura.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DbfirstContext _context;
    private TeamRepository _teamRepository;
    private DriverRepository _driverRepository;

    public UnitOfWork(DbfirstContext context)
    {
        _context = context;
    }

    public ITeam Teams
    {
        get
        {
            if (_teamRepository == null)
            {
                _teamRepository = new TeamRepository(_context);
            }

            return _teamRepository;
        }
    }

    public IDriver Drivers
    {
        get
        {
            if (_driverRepository == null)
            {
                _driverRepository = new DriverRepository(_context);
            }

            return _driverRepository;
        }
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

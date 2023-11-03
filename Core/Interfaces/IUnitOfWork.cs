namespace Core.Interfaces;

public interface IUnitOfWork
{
    IDriver Drivers { get; }
    ITeam Teams { get; }
    Task<int> SaveAsync();
}

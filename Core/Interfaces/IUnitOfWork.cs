namespace Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IEventRepository EventRepository { get; }
    IUserRepository UserRepository { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);  
    Task RollbackAsync(CancellationToken cancellationToken = default); 
}
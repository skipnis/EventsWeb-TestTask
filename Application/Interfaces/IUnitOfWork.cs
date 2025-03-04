using Core.Interfaces;

namespace Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IEventRepository EventRepository { get; }
    IUserRepository UserRepository { get; }
    IEventUserRepository EventUserRepository { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);  
    Task RollbackAsync(CancellationToken cancellationToken = default); 
}
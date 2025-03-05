using Core.Enities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IEventRepository EventRepository { get; }
    IUserRepository UserRepository { get; }
    IEventUserRepository EventUserRepository { get; }
    UserManager<User> UserManager { get; }
    RoleManager<IdentityRole<Guid>> RoleManager { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);  
    Task RollbackAsync(CancellationToken cancellationToken = default); 
}
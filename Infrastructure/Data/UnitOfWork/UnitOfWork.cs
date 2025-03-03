using Core.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction _transaction;
    private readonly IEventRepository _eventRepository;
    private readonly IUserRepository _userRepository;

    public UnitOfWork(ApplicationDbContext context, IEventRepository eventRepository, IUserRepository userRepository)
    {
        _context = context;
        _eventRepository = eventRepository;
        _userRepository = userRepository;
    }
    
    public IEventRepository EventRepository => _eventRepository;
    public IUserRepository UserRepository => _userRepository;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await BeginTransactionAsync();

        try
        {
            var result = await _context.SaveChangesAsync(cancellationToken);
            await _transaction.CommitAsync(cancellationToken);
            return result;
        }
        catch
        {
            await RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            _transaction.Dispose();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context?.Dispose();
    }
    
    private async Task BeginTransactionAsync()
    {
        if (_transaction == null)
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }
    }
}
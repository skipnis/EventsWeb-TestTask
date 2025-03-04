using Application.Interfaces;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction _transaction;
    private readonly IEventRepository _eventRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEventUserRepository _eventUserRepository;

    public UnitOfWork(ApplicationDbContext context, IEventRepository eventRepository, IUserRepository userRepository, IEventUserRepository eventUserRepository)
    {
        _context = context;
        _eventRepository = eventRepository;
        _userRepository = userRepository;
        _eventUserRepository = eventUserRepository;
    }

    public IEventRepository EventRepository => _eventRepository;
    public IUserRepository UserRepository => _userRepository;
    public IEventUserRepository EventUserRepository => _eventUserRepository;

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
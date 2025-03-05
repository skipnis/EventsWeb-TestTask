using Application.Interfaces;
using Core.Enities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction _transaction;
    private readonly IEventRepository _eventRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEventUserRepository _eventUserRepository;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public UnitOfWork(
        ApplicationDbContext context,
        IEventRepository eventRepository,
        IUserRepository userRepository,
        IEventUserRepository eventUserRepository,
        UserManager<User> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        _context = context;
        _eventRepository = eventRepository;
        _userRepository = userRepository;
        _eventUserRepository = eventUserRepository;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public IEventRepository EventRepository => _eventRepository;
    public IUserRepository UserRepository => _userRepository;
    public IEventUserRepository EventUserRepository => _eventUserRepository;
    public UserManager<User> UserManager => _userManager;
    public RoleManager<IdentityRole<Guid>> RoleManager => _roleManager;

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
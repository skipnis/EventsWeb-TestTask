using Core.Enities;

namespace Core.Interfaces;

public interface IEventRepository : IRepository<Event>
{
    public Task<Event> GetByName(string name, CancellationToken cancellationToken);
    
    public Task<IEnumerable<User>?> GetParticipants(Guid eventId, CancellationToken cancellationToken);
    
    public Task<List<Event>> GetFilteredEvents(
        DateTime? dateFrom, 
        DateTime? dateTo,
        List<string>? categories,
        List<string>? locations,
        CancellationToken cancellationToken);
}
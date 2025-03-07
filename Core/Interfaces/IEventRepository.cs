using Core.Enities;

namespace Core.Interfaces;

public interface IEventRepository : IRepository<Event>
{
    public Task<Event> GetByName(string name);
    
    public Task<IEnumerable<User>?> GetParticipants(Guid eventId);

    public Task AddImage(Guid eventId, string imageUrl);
}
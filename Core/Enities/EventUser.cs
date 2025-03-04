namespace Core.Enities;

public class EventUser
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    
    public Guid EventId { get; set; }
    public Event? Event { get; set; }
    
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
}
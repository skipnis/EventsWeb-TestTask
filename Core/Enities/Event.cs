using Core.ValueObjects;

namespace Core.Enities;

public class Event : BaseEntity<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public ICollection<Place> Places { get; set; }
    public ICollection<Category> Categories { get; set; }
    public int MaximumParticipants { get; set; }
    public DateTime Date { get; set; }
    public string ImageUrl { get; set; }
    
    public ICollection<EventUser> EventUsers { get; set; }
}
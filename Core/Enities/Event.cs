using Core.ValueObjects;

namespace Core.Enities;

public class Event : BaseEntity<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Place Place { get; set; }
    public Category Category { get; set; }
    public int MaximumParticipants { get; set; }
    public DateTime Date { get; set; }
    public string ImageUrl { get; set; }
    public ICollection<EventUser>? EventUsers { get; set; }
}
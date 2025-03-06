namespace Application.Dtos;

public class EventUpdateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int MaximumParticipants { get; set; }
    public DateTime Date { get; set; }
    public PlaceDto Place { get; set; }
    public CategoryDto Category { get; set; }
}
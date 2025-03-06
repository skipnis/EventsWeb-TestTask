namespace Application.Dtos;

public class EventCreationDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public PlaceDto Place { get; set; }
    public CategoryDto Category { get; set; }
    public DateTime Date { get; set; }
    public int MaximumParticipants { get; set; }
}
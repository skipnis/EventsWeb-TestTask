namespace Application.Dtos;

public class EventUpdateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int MaximumParticipants { get; set; }
    public DateTime Date { get; set; }
    public string ImageUrl { get; set; }
    
    public IEnumerable<PlaceDto> Places { get; set; }
    public IEnumerable<CategoryDto> Categories { get; set; }
}
namespace Application.Dtos;

public class EventFilter
{
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public List<string>? Categories { get; set; }
    public List<string>? Locations { get; set; }
}
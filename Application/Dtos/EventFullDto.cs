using Core.Enities;
using Core.ValueObjects;

namespace Application.Dtos;

public class EventFullDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<PlaceDto> Places { get; set; }
    public List<CategoryDto> Categories { get; set; }
    public DateTime Date { get; set; }
    public string ImageUrl { get; set; }
}
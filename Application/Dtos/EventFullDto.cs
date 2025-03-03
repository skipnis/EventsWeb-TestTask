using Core.Enities;
using Core.ValueObjects;

namespace Application.Dtos;

public class EventFullDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public PlaceDto Place { get; set; }
    public CategoryDto Category { get; set; }
    public DateTime Date { get; set; }
    public string ImageUrl { get; set; }
}
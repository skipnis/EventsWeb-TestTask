using Core.Enities;
using Core.ValueObjects;

namespace Application.Dtos;

public class EventFullDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Place { get; set; }
    public string Category { get; set; }
    public string Date { get; set; }
    public string? ImageUrl { get; set; }
}
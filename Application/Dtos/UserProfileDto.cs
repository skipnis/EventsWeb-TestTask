using Core.Enities;

namespace Application.Dtos;

public class UserProfileDto
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public List<EventShortDto>? Events { get; set; }
}
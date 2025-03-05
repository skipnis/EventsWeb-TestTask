using Microsoft.AspNetCore.Identity;

namespace Core.Enities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly BirthDate { get; set; }
    public ICollection<EventUser>? EventUsers { get; set; }
}
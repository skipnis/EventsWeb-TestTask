namespace Core.Enities;

public class User : BaseEntity<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime BirthDate { get; set; }
    public ICollection<EventUser> EventUsers { get; set; }
}
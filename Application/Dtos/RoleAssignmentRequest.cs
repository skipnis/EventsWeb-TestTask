namespace Application.Dtos;

public class RoleAssignmentRequest
{
    public string Role { get; set; }
    public Guid UserId { get; set; }
}
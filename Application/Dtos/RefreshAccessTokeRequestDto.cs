namespace Application.Dtos;

public class RefreshAccessTokeRequestDto
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public IEnumerable<string> Roles { get; set; }
    public string RefreshToken { get; set; }
}
namespace Application.Dtos;

public class RefreshAccessTokenDto
{
    public string UserId { get; set; }
    public string RefreshToken { get; set; }
}
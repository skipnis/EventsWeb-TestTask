using Microsoft.AspNetCore.Http;

namespace Application.Dtos;

public class AddEventImageDto
{
    public IFormFile Image { get; set; }
}
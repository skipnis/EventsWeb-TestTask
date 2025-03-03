using Application.Interfaces;

namespace Application.Utils;

public class EmailContentGenerator : IEmailContentGenerator
{
    public string GenerateEventUpdateContent(string name, string eventTitle)
    {
        return $"<html><body><h3>Dear {name},</h3><p>The event '{eventTitle}' has been updated.</p></body></html>";
    }
}
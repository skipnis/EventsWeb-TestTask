namespace Application.Interfaces;

public interface IEmailContentGenerator
{
    string GenerateEventUpdateContent(string name, string eventTitle);
}
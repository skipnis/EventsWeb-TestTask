using Application.Interfaces;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    public  Task SendEmailAsync(string email, string subject, string message)
    {
        // TODO EmailService
         Console.WriteLine("Sending email...");
         return Task.CompletedTask;
    }
}
using Application.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly string _apiKey = "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";
    private readonly string _senderEmil = "kipnis_am_22@mf.grsu.by";
    
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var client = new SendGridClient(_apiKey);
        
        var from = new EmailAddress(_senderEmil, "EventsWeb"); 
        var to = new EmailAddress(email);
        
        var plainTextContent = message; 
        var htmlContent = $"<p>{message}</p>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        
        await client.SendEmailAsync(msg);
    }
}
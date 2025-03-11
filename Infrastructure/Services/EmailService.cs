using Application.Interfaces;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly EmailServiceSettings _settings;

    public EmailService(IOptionsSnapshot<EmailServiceSettings> options)
    {
        _settings = options.Value ?? throw new ArgumentNullException(nameof(options));
    }
    public async Task SendEmailAsync(string email, string subject, string message, CancellationToken cancellationToken)
    {
        var client = new SendGridClient(_settings.ApiKey);
        
        var from = new EmailAddress(_settings.SenderEmail, "EventsWeb"); 
        var to = new EmailAddress(email);
        
        var plainTextContent = message; 
        var htmlContent = $"<p>{message}</p>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        
        await client.SendEmailAsync(msg, cancellationToken);
    }
}
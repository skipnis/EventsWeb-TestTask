using Application.Events;
using Application.Interfaces;
using Core.Interfaces;
using MediatR;

namespace Application.Handlers.EventHandlers;

public class EventUpdatedHandler : INotificationHandler<EventUpdated>
{
    private readonly IEmailService _emailService;
    private readonly IEmailContentGenerator _emailContentGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public EventUpdatedHandler(IEmailService emailService, IEmailContentGenerator emailContentGenerator, IUnitOfWork unitOfWork)
    {
        _emailService = emailService;
        _emailContentGenerator = emailContentGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(EventUpdated notification, CancellationToken cancellationToken)
    {
        var eventEntity = await _unitOfWork.EventRepository.GetById(notification.EventId);
        
        if (eventEntity == null)
            throw new Exception("Event not found");
        
        var usersToNotify = eventEntity.EventUsers?
            .Where(x => x.User != null)
            .Select(x => x.User).ToList();

        if (usersToNotify != null && usersToNotify.Any())
        {
            foreach (var user in usersToNotify)
            {
                var message = _emailContentGenerator.GenerateEventUpdateContent(user.FirstName, eventEntity.Title);
                await _emailService.SendEmailAsync(user.Email, "Event Update Notification", message);
            }
        }
    }
}
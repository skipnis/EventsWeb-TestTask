using MediatR;

namespace Application.Commands.EventCommands;

public class DeleteEventCommand : IRequest<bool>
{
    public Guid Id { get; set; }

    public DeleteEventCommand(Guid id)
    {
        Id = id;
    }
}
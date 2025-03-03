using Application.Dtos;
using MediatR;

namespace Application.Commands.UserCommands;

public class UpdateUserCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public UserUpdateDto UserUpdateDto { get; set; }

    public UpdateUserCommand(Guid id, UserUpdateDto user)
    {
        Id = id;
        UserUpdateDto = user;
    }
}
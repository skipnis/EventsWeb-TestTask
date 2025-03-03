using Application.Dtos;
using MediatR;

namespace Application.Queries;

public class GetUserProfileQuery : IRequest<UserProfileDto>
{
    public Guid Id { get; set; }

    public GetUserProfileQuery(Guid id)
    {
        Id = id;
    }
}
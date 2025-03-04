using Application.Dtos;
using Core.Enities;
using MediatR;

namespace Application.Queries;

public class GetFilteredEventsQuery : IRequest<IEnumerable<EventFullDto>>
{
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public List<string>? Categories { get; set; }
    public List<string>? Locations { get; set; }

    public GetFilteredEventsQuery(DateTime? dateFrom, DateTime? dateTo, List<string>? categories, List<string>? locations)
    {
        DateFrom = dateFrom;
        DateTo = dateTo;
        Categories = categories;
        Locations = locations;
    }
}
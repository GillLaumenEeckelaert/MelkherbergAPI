using Contracts.Shows.Queries;
using Contracts.Shows.Shared;
using Database;
using Framework.Handlers;
using Microsoft.EntityFrameworkCore;
using Models.Shows;

namespace Handlers.Shows;

public class GetEventsHandler(ApplicationDbContext db) : AuthenticatedQueryHandler<GetEvents, GetEventsResponse>
{
    protected override async Task<GetEventsResponse> Execute(GetEvents request)
    {
        Console.WriteLine("Get Events");
        var events = db.Event.Where(e => e.EventId != Guid.Empty);

        var eventDtos = await (Mapper.ProjectTo<EventSummaryDto>(events)).ToListAsync();

        foreach (var eventDto in eventDtos)
        {
            if (eventDto.LocationId is not null && eventDto.LocationId != Guid.Empty)
            {
                var location = await db.Location.FirstOrDefaultAsync(l => l.LocationId == eventDto.LocationId && (l.ValidFrom == null || l.ValidFrom <= DateOnly.FromDateTime(DateTime.Today)) && (l.ValidTo == null || l.ValidTo >= DateOnly.FromDateTime(DateTime.Today)));
                eventDto.LocationName = location?.Name;
            }
        }

        return new GetEventsResponse() { Events = eventDtos };
    }
}
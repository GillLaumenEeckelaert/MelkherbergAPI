using Contracts.Shows.Queries;
using Contracts.Shows.Shared;
using Database;
using Framework.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Handlers.Shows;

public class GetEventHandler(ApplicationDbContext db) : QueryHandler<GetEvent, GetEventResponse>
{
    protected override async Task<GetEventResponse> Execute(GetEvent request)
    {
        var selectedEvent = db.Event.Where(e => e.EventId == request.EventId);
        var eventDto = await Mapper.ProjectTo<EventDto>(selectedEvent).SingleAsync();

        if (eventDto.Location?.LocationId is not null && eventDto.Location.LocationId != Guid.Empty)
        {
            var location = await db.Location.SingleOrDefaultAsync(l => l.LocationId == eventDto.Location.LocationId && l.ValidFrom <= DateOnly.FromDateTime(DateTime.Now) && (l.ValidTo == null || l.ValidTo > DateOnly.FromDateTime(DateTime.Now)));
            eventDto.Location = Mapper.Map<LocationDto>(location);
        }
        
        return new GetEventResponse() { Event = eventDto };
    }
}
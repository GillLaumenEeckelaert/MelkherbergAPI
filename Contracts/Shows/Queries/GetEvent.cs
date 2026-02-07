using Contracts.Shows.Shared;

namespace Contracts.Shows.Queries;

public class GetEvent
{
    public Guid EventId { get; set; }
}

public class GetEventResponse
{
    public EventDto Event { get; set; } = new();
}
using Contracts.Shows.Shared;
using Framework.Attributes;

namespace Contracts.Shows.Queries;

[Authenticate]
public class GetEvents
{
    public List<Guid> PersonIds { get; set; } = [];
}

public class GetEventsResponse
{
    public List<EventSummaryDto> Events { get; set; } = [];
}
using Contracts.Shows.Shared;

namespace Contracts.Shows.Queries;

public class GetShows
{
}

public class GetShowsResponse
{
    public List<ShowSummaryDto> Shows { get; set; } = [];
}
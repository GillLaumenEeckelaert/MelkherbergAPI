using Contracts.Shows.Queries;
using Contracts.Shows.Shared;
using Database;
using Framework.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Handlers.Shows;

public class GetShowsHandler(ApplicationDbContext db) : QueryHandler<GetShows, GetShowsResponse>
{
    protected override async Task<GetShowsResponse> Execute(GetShows request)
    {
        var shows = db.Show.Where(e => e.ShowId != Guid.Empty);
        return new GetShowsResponse { Shows = await (Mapper.ProjectTo<ShowSummaryDto>(shows)).ToListAsync() };
    }
}
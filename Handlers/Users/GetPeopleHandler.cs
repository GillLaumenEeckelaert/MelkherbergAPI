using Contracts.Users.Queries;
using Contracts.Users.Shared;
using Database;
using Framework.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Handlers.Users;

public class GetPeopleHandler(ApplicationDbContext db) : QueryHandler<GetPeople, GetPeopleResponse>
{
    protected override async Task<GetPeopleResponse> Execute(GetPeople request)
    {
        var people = db.Person.Where(p => p.IsSelectable && p.PersonId != Guid.Empty);
        return new GetPeopleResponse
        {
            People = await Mapper.ProjectTo<PersonSummaryDto>(people).ToListAsync()
        };
    }
}
using Contracts.Users.Shared;

namespace Contracts.Users.Queries;

public class GetPeople
{
}

public class GetPeopleResponse
{
    public List<PersonSummaryDto> People { get; set; } = [];
}
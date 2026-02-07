namespace Contracts.Users.Shared;

public class PersonSummaryDto
{
    public Guid PersonId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
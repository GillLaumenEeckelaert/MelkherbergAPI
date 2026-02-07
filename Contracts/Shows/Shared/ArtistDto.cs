using Contracts.Users.Shared;

namespace Contracts.Shows.Shared;

public class ArtistDto
{
    public Guid ArtistId { get; set; }
    
    public string? Name { get; set; }
    public string? Role { get; set; }
    public PersonSummaryDto? Person { get; set; } 
}
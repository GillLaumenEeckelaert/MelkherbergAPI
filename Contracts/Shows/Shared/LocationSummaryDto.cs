namespace Contracts.Shows.Shared;

public class LocationSummaryDto
{
    public Guid LocationId { get; set; }
    
    public string Name { get; set; } = string.Empty;
}
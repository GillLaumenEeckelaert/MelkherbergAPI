using Codes.Location;

namespace Contracts.Shows.Shared;

public class LocationDto
{
    public Guid LocationId { get; set; }
    public Guid LocationVersionId { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    
    public LocationType LocationType { get; set; }
    
    public string? Street1 { get; set; }
    public string? Street2 { get; set; }
    public string? Number { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
}
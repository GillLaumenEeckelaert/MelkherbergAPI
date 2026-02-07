using Codes.General;

namespace Contracts.Shows.Shared;

public class EventDto
{
    public Guid EventId { get; set; }
    
    public string? EventName { get; set; }
    public string? EventSubName { get; set; }
    
    public Language Language { get; set; }
    
    public DateType DateType { get; set; }
    public DateTime Start { get; set; }
    public DateTime? End { get; set; }
    
    public LocationDto? Location { get; set; }
    public ShowDto? Show { get; set; }
    public List<TicketDto> Tickets { get; set; } = [];
    public List<ArtistDto> Artists { get; set; } = [];
}
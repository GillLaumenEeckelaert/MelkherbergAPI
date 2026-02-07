using Codes.General;

namespace Contracts.Shows.Shared;

public class EventSummaryDto
{
    public Guid EventId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? SubName { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateType DateType { get; set; }
    
    public Guid? LocationId { get; set; }
    public string? LocationName { get; set; }
    
    public List<TicketSummaryDto> Tickets { get; set; } = [];
}
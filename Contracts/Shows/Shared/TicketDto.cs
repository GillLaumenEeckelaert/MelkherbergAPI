using Codes.General;
using Codes.Tickets;
using Contracts.Users.Shared;

namespace Contracts.Shows.Shared;

public class TicketDto
{
    public Guid TicketId { get; set; }
    
    public PersonSummaryDto? Person { get; set; }
    
    public decimal? Price { get; set; }
    public Currency Currency { get; set; }
    
    public string? Section { get; set; }
    public string? Row { get; set; }
    public string? Seat { get; set; }

    public TicketType Type { get; set; }
    public string? Comment { get; set; }
}
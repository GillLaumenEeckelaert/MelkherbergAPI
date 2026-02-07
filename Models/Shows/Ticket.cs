using System.ComponentModel.DataAnnotations;
using Codes.General;
using Codes.Tickets;
using Framework.Models;
using Models.General;

namespace Models.Shows;

public class Ticket : BaseModel
{
    [Key]
    public Guid TicketId { get; set; } = Guid.NewGuid();
    
    public Guid? PersonId { get; set; }
    public Guid EventId { get; set; }
    
    public decimal? Price { get; set; }
    public Currency Currency { get; set; } = Currency.Euro;
    
    public string? Section { get; set; }
    public string? Row { get; set; }
    public string? Seat { get; set; }

    public TicketType Type { get; set; } = TicketType.Full;
    public string? Comment { get; set; }
    
    public virtual Person? Person { get; set; }
    public virtual required Event Event { get; set; }
}
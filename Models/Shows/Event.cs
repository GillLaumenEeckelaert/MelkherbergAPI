using System.ComponentModel.DataAnnotations;
using Codes.General;
using Framework.Models;
using Models.General;

namespace Models.Shows;

public class Event : BaseModel
{
    [Key]
    public Guid EventId { get; set; } = Guid.NewGuid();
    
    public DateType DateType { get; set; } = DateType.Exact;
    public DateTime StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    
    public string? EventName { get; set; }
    public string? EventSubName { get; set; }

    public Language Language { get; set; } = Language.Nl;
    
    public Guid? LocationId { get; set; }
    public Guid? ShowId { get; set; }
    
    public virtual Show? Show { get; set; }

    public List<Ticket> Tickets { get; set; } = [];
    public List<EventArtist> EventArtists { get; set; } = [];
}
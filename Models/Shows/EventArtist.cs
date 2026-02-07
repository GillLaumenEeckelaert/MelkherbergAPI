using System.ComponentModel.DataAnnotations;
using Framework.Models;
using Models.General;

namespace Models.Shows;

public class EventArtist : BaseModel
{
    [Key]
    public Guid EventArtistId { get; set; } = Guid.NewGuid();
    
    public Guid EventId { get; set; }
    
    public string? Role { get; set; }
    
    public virtual required Artist Artist { get; set; }
    public virtual required Event Event { get; set; }
}
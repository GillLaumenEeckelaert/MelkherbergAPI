using System.ComponentModel.DataAnnotations;
using Framework.Models;
using Models.General;

namespace Models.Shows;

public class ArtistPerson : BaseModel
{
    [Key]
    public Guid ArtistPersonId { get; set; } = Guid.NewGuid();
    
    public Guid PersonId { get; set; }
    
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    
    public virtual required Artist Artist { get; set; }
    public virtual required Person Person { get; set; }
}
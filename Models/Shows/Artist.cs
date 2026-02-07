using System.ComponentModel.DataAnnotations;
using Framework.Models;

namespace Models.Shows;

public class Artist : BaseModel
{
    [Key]
    public Guid ArtistVersionId { get; set; } = Guid.NewGuid();
    public Guid ArtistId { get; set; } = Guid.NewGuid();
    
    public required string Name { get; set; }
    public string? SubName { get; set; }
}
using System.ComponentModel.DataAnnotations;
using Codes.Shows;
using Framework.Models;

namespace Models.Shows;

public class Show : BaseModel
{
    [Key]
    public Guid ShowId { get; set; } = Guid.NewGuid();
    
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    
    public ShowType ShowType { get; set; }
}
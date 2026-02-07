using System.ComponentModel.DataAnnotations;
using Codes.Location;
using Framework.Models;

namespace Models.General;

public class Location : BaseModel
{
    [Key]
    public Guid LocationVersionId { get; set; } = Guid.NewGuid();
    public Guid LocationId { get; set; } = Guid.NewGuid();
    
    public DateOnly? ValidFrom { get; set; }
    public DateOnly? ValidTo { get; set; }
    
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    
    public LocationType LocationType { get; set; }
    
    [StringLength(150)]
    public string? Street1 { get; set; }
    [StringLength(255)]
    public string? Street2 { get; set; }
    [StringLength(50)]
    public string? Number { get; set; }
    [StringLength(50)]
    public string? PostalCode { get; set; }
    [StringLength(150)]
    public string? City { get; set; }
    [StringLength(150)]
    public string? State { get; set; }
    [StringLength(3)]
    public string? Country { get; set; }
}
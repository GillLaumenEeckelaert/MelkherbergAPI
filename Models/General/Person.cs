using System.ComponentModel.DataAnnotations;
using Framework.Models;

namespace Models.General;

public class Person : BaseModel
{
    [Key]
    public Guid PersonId { get; set; } = Guid.NewGuid();
    
    public Guid? UserId { get; set; }

    public bool IsSelectable { get; set; } = false;
    
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public string? NickName { get; set; }
    
    public DateOnly? Birthday { get; set; }
}
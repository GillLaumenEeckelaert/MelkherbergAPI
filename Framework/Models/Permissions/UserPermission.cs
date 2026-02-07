namespace Framework.Models.Permissions;

public class UserPermission : ValidInTimeModel
{
    public Guid UserPermissionId { get; set; }
    
    public Guid UserId { get; set; }
    public Guid PermissionId { get; set; }

    public virtual Permission Permission { get; set; } = null!;
}
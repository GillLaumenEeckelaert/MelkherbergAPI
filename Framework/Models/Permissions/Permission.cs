namespace Framework.Models.Permissions;

public class Permission : BaseModel
{
    public Guid PermissionId { get; set; }

    public string PermissionCode { get; set; } = null!;

    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public string Category { get; set; } = "Overig";
}
namespace Framework.Contracts.Permissions.Shared;

public class PermissionDto
{
    public Guid PermissionId { get; set; }

    public string PermissionCode { get; set; } = null!;

    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public string Category { get; set; } = null!;
}
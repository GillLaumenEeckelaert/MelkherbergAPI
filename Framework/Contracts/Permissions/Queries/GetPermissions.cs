using Framework.Contracts.Permissions.Shared;

namespace Framework.Contracts.Permissions.Queries;

public class GetPermissions
{
}

public class GetPermissionsResponse
{
    public List<PermissionDto> Permissions { get; set; } = [];
}
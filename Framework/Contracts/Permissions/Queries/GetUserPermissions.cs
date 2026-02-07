using Framework.Contracts.Permissions.Shared;

namespace Framework.Contracts.Permissions.Queries;

public class GetUserPermissions
{
}

public class GetUserPermissionsResponse
{
    public List<UserPermissionDto> UserPermissions { get; set; } = [];
}
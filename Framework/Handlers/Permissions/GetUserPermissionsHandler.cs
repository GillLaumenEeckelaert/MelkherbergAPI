using Framework.Contracts.Permissions.Queries;
using Framework.Contracts.Permissions.Shared;
using Framework.Database;
using Microsoft.EntityFrameworkCore;

namespace Framework.Handlers.Permissions;

public class GetPermissionsHandler(FrameworkDbContext db) : AuthenticatedQueryHandler<GetPermissions, GetPermissionsResponse>
{
    protected override async Task<GetPermissionsResponse> Execute(GetPermissions request)
    {
        var permissions = db.Permission;

        var permissionDtos = await (Mapper.ProjectTo<PermissionDto>(permissions)).ToListAsync();

        return new GetPermissionsResponse() { Permissions = permissionDtos };
    }
}
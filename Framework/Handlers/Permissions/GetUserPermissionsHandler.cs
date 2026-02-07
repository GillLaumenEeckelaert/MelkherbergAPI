using System.Security.Claims;
using Framework.Contracts.Permissions.Queries;
using Framework.Contracts.Permissions.Shared;
using Framework.Database;
using Microsoft.EntityFrameworkCore;

namespace Framework.Handlers.Permissions;

public class GetUserPermissionsHandler(FrameworkDbContext db) : QueryHandler<GetUserPermissions, GetUserPermissionsResponse>
{
    protected override async Task<GetUserPermissionsResponse> Execute(GetUserPermissions request)
    {
        var userIdKnown = Guid.TryParse(HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty, out var userId);

        if (!userIdKnown)
        {
            return new GetUserPermissionsResponse();
        }
        var permissions = db.Permission;
        var userPermissions = db.UserPermission.Where(up => up.UserId == userId && up.ValidFrom < DateTime.UtcNow && up.ValidTo > DateTime.UtcNow).ToList();

        Console.WriteLine(userId);
        Console.WriteLine(userPermissions.Count);
        
        var userPermissionDtos = await (Mapper.ProjectTo<UserPermissionDto>(permissions)).ToListAsync();

        foreach (var userPermissionDto in userPermissionDtos)
        {
            userPermissionDto.UserPermissionId = userPermissions.FirstOrDefault(up => up.PermissionId == userPermissionDto.PermissionId)?.UserPermissionId;
        }

        return new GetUserPermissionsResponse() { UserPermissions = userPermissionDtos };
    }
}
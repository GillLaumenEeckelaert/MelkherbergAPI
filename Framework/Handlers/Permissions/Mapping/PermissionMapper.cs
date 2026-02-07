using AutoMapper;
using Framework.Contracts.Permissions.Shared;
using Framework.Models.Permissions;

namespace Framework.Handlers.Permissions.Mapping;

public class PermissionMapper : Profile
{
    public PermissionMapper()
    {
        CreateMap<Permission, PermissionDto>();
        CreateMap<Permission, UserPermissionDto>()
            .ForMember(dest => dest.UserPermissionId, opt => opt.Ignore());
    }
}
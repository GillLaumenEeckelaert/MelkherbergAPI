using AutoMapper;
using Contracts.Users.Shared;
using Models.General;

namespace Handlers.Users.Mapping;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<Person, PersonSummaryDto>();
    }
}
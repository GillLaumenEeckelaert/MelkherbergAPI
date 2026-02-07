using AutoMapper;
using Contracts.Shows.Shared;
using Models.General;

namespace Handlers.General.Mapping;

public class LocationMapper : Profile
{
    public LocationMapper()
    {
        CreateMap<Location, LocationDto>();
        CreateMap<Guid?, LocationDto>()
            .ForMember(dest => dest.LocationId, opt => opt.MapFrom(src => src ?? Guid.Empty))
            .ForMember(dest => dest.LocationVersionId, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.Ignore())
            .ForMember(dest => dest.Description, opt => opt.Ignore())
            .ForMember(dest => dest.Street1, opt => opt.Ignore())
            .ForMember(dest => dest.Street2, opt => opt.Ignore())
            .ForMember(dest => dest.City, opt => opt.Ignore())
            .ForMember(dest => dest.State, opt => opt.Ignore())
            .ForMember(dest => dest.PostalCode, opt => opt.Ignore())
            .ForMember(dest => dest.Country, opt => opt.Ignore())
            .ForMember(dest => dest.Number, opt => opt.Ignore());
    }
}

public static class MappingExpressionExtensions
{
    public static IMappingExpression<TSource, TDest> IgnoreAllUnmapped<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
    {
        expression.ForAllMembers(opt => opt.Ignore());
        return expression;
    }
}
using AutoMapper;
using Contracts.Shows.Shared;
using Models.Shows;

namespace Handlers.Shows.Mapping;

public class ShowMapper : Profile
{
    public ShowMapper()
    {
        CreateMap<Event, EventSummaryDto>()
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => string.IsNullOrEmpty(src.EventName) ? (src.Show != null ? src.Show.Name : string.Empty) : src.EventName))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDateTime))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDateTime))
            .ForMember(dest => dest.Tickets, opt => opt.MapFrom(src => src.Tickets));
        CreateMap<Event, EventDto>()
            .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.StartDateTime))
            .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.EndDateTime))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.LocationId))
            .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.EventArtists));
        CreateMap<Ticket, TicketSummaryDto>();
        CreateMap<Ticket, TicketDto>();
        CreateMap<Show, ShowSummaryDto>();
        CreateMap<Show, ShowDto>();
        CreateMap<EventArtist, ArtistDto>()
            .ForMember(dest => dest.ArtistId, opt => opt.MapFrom(src => src.Artist.ArtistId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Artist.Name))
            .ForMember(dest => dest.Person, opt => opt.Ignore());
    }
}
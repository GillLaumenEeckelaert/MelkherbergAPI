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
        CreateMap<Ticket, TicketSummaryDto>();
        CreateMap<Show, ShowSummaryDto>();
    }
}
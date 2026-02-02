using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;

namespace AuctionService.RequestHelpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Auction, AuctionDTO>().IncludeMembers(x => x.Item)
                .ForMember(d => d.CreatedAt, s => s.MapFrom(x => x.CreatedAt))
                .ForMember(d => d.ModifiedAt, s => s.MapFrom(x => x.ModifiedAt));
            CreateMap<Item, AuctionDTO>();
        }
    }
}

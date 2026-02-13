using AuctionService.Entities;
using AuctionService.Features.Auctions.DTOs;
using AutoMapper;
using Contracts;

namespace AuctionService.RequestHelpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Auction, AuctionDTO>().IncludeMembers(x => x.Item)
                .ForMember(d => d.Slug_Id, s => s.MapFrom(x => x.Id))
                .ForMember(d => d.CreatedAt, s => s.MapFrom(x => x.CreatedAt))
                .ForMember(d => d.ModifiedAt, s => s.MapFrom(x => x.ModifiedAt));
            CreateMap<Item, AuctionDTO>();
            CreateMap<CreateAuctionDTO, Auction>();
            CreateMap<CreateAuctionDTO, Item>();
            CreateMap<AuctionDTO, AuctionCreated>();
            CreateMap<AuctionDTO, AuctionUpdated>();
            CreateMap<AuctionDTO,AuctionDeleted>();
        }
    }
}

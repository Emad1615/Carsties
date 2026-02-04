using AuctionService.Features.Auctions.Commands;
using AuctionService.Features.Auctions.DTOs;
using FluentValidation;

namespace AuctionService.Features.Auction.Validators
{
    public class CreateAuctionValidator : BaseAuctionValidator<AddAuction.Command, CreateAuctionDTO>
    {
        public CreateAuctionValidator() : base(x => x.AuctionDTO)
        {
            
        }
    }
}

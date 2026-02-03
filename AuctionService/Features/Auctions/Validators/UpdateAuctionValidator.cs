using AuctionService.Features.Auctions.Commands;
using AuctionService.Features.Auctions.DTOs;
using FluentValidation;

namespace AuctionService.Features.Auction.Validators
{
    public class UpdateAuctionValidator : BaseAuctionValidator<UpdateAuction.Command, UpdateAuctionDTO>
    {
        public UpdateAuctionValidator() : base(x => x.AuctionDTO)
        {
            RuleFor(x => x.AuctionDTO.Id)
                .NotEmpty().WithMessage("ReservePrice is required.");
        }
    }
}

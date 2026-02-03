using AuctionService.Features.Auctions.Commands;
using AuctionService.Features.Auctions.DTOs;
using FluentValidation;

namespace AuctionService.Features.Auction.Validators
{
    public class CreateAuctionValidator : BaseAuctionValidator<AddAuction.Command, CreateAuctionDTO>
    {
        public CreateAuctionValidator() : base(x => x.AuctionDTO)
        {
            RuleFor(x => x.AuctionDTO.ReservePrice).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("ReservePrice is required.")
                .GreaterThan(0).WithMessage("ReservePrice cannot be negative.");
            RuleFor(x => x.AuctionDTO.AuctionEnd).Cascade(CascadeMode.Stop)
                .GreaterThan(DateTime.UtcNow).WithMessage("AuctionEnd must be a future date.")
                .NotEmpty().WithMessage("AuctionEnd is required.");
            RuleFor(x => x.AuctionDTO.ImageUrl)
                .NotEmpty().WithMessage("ImageUrl is required.");
        }
    }
}

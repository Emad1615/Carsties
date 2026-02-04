using AuctionService.Features.Auctions.DTOs;
using FluentValidation;

namespace AuctionService.Features.Auction.Validators
{
    public class BaseAuctionValidator<T, DTO> : AbstractValidator<T> where DTO : BaseAuctionDTO
    {
        public BaseAuctionValidator(Func<T, DTO> selector)
        {
            RuleFor(x => selector(x).ReservePrice).Cascade(CascadeMode.Stop)
               .NotEmpty().WithMessage("ReservePrice is required.")
               .GreaterThan(0).WithMessage("ReservePrice cannot be negative.");
            RuleFor(x => selector(x).AuctionEnd).Cascade(CascadeMode.Stop)
                .GreaterThan(DateTime.UtcNow).WithMessage("AuctionEnd must be a future date.")
                .NotEmpty().WithMessage("AuctionEnd is required.");
            RuleFor(x => selector(x).Color).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Color is required.")
                .MaximumLength(50).WithMessage("Color cannot exceed 50 characters.");
            RuleFor(x => selector(x).Make).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Make is required.")
                .MaximumLength(50).WithMessage("Make cannot exceed 50 characters.");
            RuleFor(x => selector(x).Model).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Model is required.")
                .MaximumLength(50).WithMessage("Model cannot exceed 50 characters.");
            RuleFor(x => selector(x).Year).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Year is required.")
                .InclusiveBetween(1886, DateTime.UtcNow.Year + 1).WithMessage($"Year must be between 1886 and {DateTime.UtcNow.Year + 1}.");
            RuleFor(x => selector(x).Mileage).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Mileage is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Mileage cannot be negative.");
            RuleFor(x => selector(x).ImageUrl)
                .NotEmpty().WithMessage("ImageUrl is required.");
        }
    }
}

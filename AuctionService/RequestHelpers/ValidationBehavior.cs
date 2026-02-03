using FluentValidation;
using MediatR;

namespace AuctionService.RequestHelpers
{
    public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest> validator = null) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validator is null) await next();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            return await next();
        }
    }
}

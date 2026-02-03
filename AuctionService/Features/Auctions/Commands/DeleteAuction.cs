using AuctionService.Data;
using MediatR;

namespace AuctionService.Features.Auctions.Commands
{
    public class DeleteAuction
    {
        public class Command : IRequest<Unit>
        {
            public int Id { get; set; }
        }
        public class Handler(AuctionDbContext context) : IRequestHandler<Command, Unit>
        {
            public Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}

using DataStockService.Core.Entities;
using DataStockService.Infrastructure.Context;
using MediatR;

namespace DataStockService.App.Handlers.ProductHandlers
{
    public class CreateProductCommand : IRequest
    {
    }

    public class CreateProductCommandHandler(AppDbContext context) : IRequestHandler<CreateProductCommand>
    {
        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product("132");
            var outboxEvent = new OutboxEvent("topic", "key", "Product", "payload");

            await context.Products.AddAsync(product, cancellationToken);
            await context.OutboxEvents.AddAsync(outboxEvent, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}

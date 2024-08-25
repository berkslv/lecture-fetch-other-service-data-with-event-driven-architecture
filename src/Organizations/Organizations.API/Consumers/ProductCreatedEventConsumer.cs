using MassTransit;
using MediatR;
using Organizations.Application.Features.Products.ProductCreated;
using Shared.Contracts;

namespace Organizations.API.Consumers;

public class ProductCreatedEventConsumer : IConsumer<ProductCreatedEvent>
{
    private readonly ISender _mediatr;

    public ProductCreatedEventConsumer(ISender mediatr)
    {
        _mediatr = mediatr;
    }

    public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
    {
        var message = context.Message;
        await _mediatr.Send(new ProductCreatedCommand
        {
            Id = message.Id,
            Name =  message.Name,
        });
       
    }
}

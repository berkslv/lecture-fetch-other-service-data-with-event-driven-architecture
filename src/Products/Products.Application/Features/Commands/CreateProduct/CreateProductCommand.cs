using MassTransit;
using MediatR;
using Products.Application.Interfaces;
using Products.Domain.Entities;
using Shared.Contracts;

namespace Products.Application.Features.Commands.CreateProduct;

public record CreateProductCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateProductCommandHandler(IApplicationDbContext context, IPublishEndpoint publishEndpoint)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name
        };

        await _context.Products.AddAsync(product, cancellationToken);
                
        product.AddDomainEvent(new ProductCreatedEvent
        {
            Id = product.Id,
            Name = product.Name
        });
        
        await _context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
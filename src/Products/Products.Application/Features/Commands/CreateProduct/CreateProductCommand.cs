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

    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name
        };
        
        product.AddDomainEvent(new ProductCreatedEvent
        {
            Id = product.Id,
            Name = product.Name
        });

        _context.Products.Add(product);
        
        await _context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
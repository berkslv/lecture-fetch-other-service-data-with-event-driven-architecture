using MediatR;
using Organizations.Application.Interfaces;
using Organizations.Domain.Entities;

namespace Organizations.Application.Features.Products.ProductCreated;

public record ProductCreatedCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class ProductCreatedCommandHandler : IRequestHandler<ProductCreatedCommand>
{
    private readonly IApplicationDbContext _context;

    public ProductCreatedCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ProductCreatedCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Id = request.Id,
            Name = request.Name
        };

        _context.Products.Add(product);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
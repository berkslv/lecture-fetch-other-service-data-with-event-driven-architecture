using MediatR;
using Products.Application.Interfaces;
using Products.Domain.Exceptions;

namespace Products.Application.Features.Queries.GetProductById;

public record GetProductByIdQuery : IRequest<GetProductByIdQueryResponse>
{
    public Guid Id { get; set; }
}

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResponse>
{
    private readonly IApplicationDbContext _context;

    public GetProductByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(request.Id);

        if (product is null)
        {
            throw new BadRequestException("Product not found");
        }

        return new GetProductByIdQueryResponse
        {
            Id = product.Id,
            Name = product.Name
        };
    }
}



using MediatR;
using Microsoft.EntityFrameworkCore;
using Products.Application.Interfaces;

namespace Products.Application.Features.Queries.GetProducts;

public record GetProductsQuery : IRequest<List<GetProductsQueryResponse>>;


public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<GetProductsQueryResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetProductsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<GetProductsQueryResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Products.ToListAsync(cancellationToken);

        return products.Select(product => new GetProductsQueryResponse
        {
            Id = product.Id,
            Name = product.Name
        }).ToList();
    }
}

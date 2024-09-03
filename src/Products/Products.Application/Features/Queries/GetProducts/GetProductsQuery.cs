using MediatR;
using Microsoft.EntityFrameworkCore;
using Products.Application.Interfaces;

namespace Products.Application.Features.Queries.GetProducts;

public record GetProductsQuery : IRequest<List<GetProductsQueryResponse>>;


public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<GetProductsQueryResponse>>
{

    public GetProductsQueryHandler()
    {
    }

    public async Task<List<GetProductsQueryResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = new List<GetProductsQueryResponse>
        {
            new GetProductsQueryResponse
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Created = DateTime.Now
            },
            new GetProductsQueryResponse
            {
                Id = Guid.NewGuid(),
                Name = "Product 2",
                Created = DateTime.Now
            }
        };
        
        return products.Select(product => new GetProductsQueryResponse
        {
            Id = product.Id,
            Name = product.Name
        }).ToList();
    }
}

using Microsoft.AspNetCore.Mvc;
using Products.API.Controllers.Base;
using Products.Application.Features.Commands.CreateProduct;
using Products.Application.Features.Queries.GetProductById;
using Products.Application.Features.Queries.GetProducts;

namespace Products.API.Controllers;

[Route("api/products")]
public class ProductsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateUnit([FromQuery] CreateProductCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<GetProductByIdQueryResponse>> GetProductById([FromRoute] Guid id)
    {
        var query = new GetProductByIdQuery
        {
            Id = id
        };
        
        return await Mediator.Send(query);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<GetProductsQueryResponse>>> GetProducts()
    {
        return await Mediator.Send(new GetProductsQuery());
    }
}
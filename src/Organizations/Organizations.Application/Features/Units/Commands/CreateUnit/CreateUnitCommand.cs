using MediatR;
using Organizations.Application.Interfaces;
using Organizations.Domain.Exceptions;
using Unit = Organizations.Domain.Entities.Unit;

namespace Organizations.Application.Features.Units.Commands.CreateUnit;

public record CreateUnitCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    
    public List<Guid> ProductIdList { get; set; } = new();
}

public class CreateUnitCommandHandler : IRequestHandler<CreateUnitCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IProductService _productService;

    public CreateUnitCommandHandler(IApplicationDbContext context, IProductService productService)
    {
        _context = context;
        _productService = productService;
    }

    public async Task<Guid> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
    {
        foreach (var productId in request.ProductIdList)
        {
            var isProductExist = await _productService.GetProductById(productId);
        
            if (!isProductExist.IsSuccessStatusCode)
            {
                throw new BadRequestException("Product not found");
            }
        }

        var unit = new Unit
        {
            Name = request.Name,
        };
        
        _context.Units.Add(unit);

        await _context.SaveChangesAsync(cancellationToken);

        return unit.Id;
    }
}
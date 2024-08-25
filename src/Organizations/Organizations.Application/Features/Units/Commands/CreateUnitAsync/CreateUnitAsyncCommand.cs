using MediatR;
using Microsoft.EntityFrameworkCore;
using Organizations.Application.Interfaces;
using Organizations.Domain.Exceptions;
using Unit = Organizations.Domain.Entities.Unit;

namespace Organizations.Application.Features.Units.Commands.CreateUnitAsync;

public record CreateUnitAsyncCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    
    public List<Guid> ProductIdList { get; set; } = new();
}

public class CreateUnitAsyncCommandHandler : IRequestHandler<CreateUnitAsyncCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateUnitAsyncCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateUnitAsyncCommand request, CancellationToken cancellationToken)
    {
        // check if _context.Products contains all products with Id in request.ProductIdList
        var products = await _context.Products
            .Where(p => request.ProductIdList.Contains(p.Id))
            .ToListAsync(cancellationToken);

        if (products.Count != request.ProductIdList.Count)
        {
            throw new BadRequestException("Some products are not found");
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
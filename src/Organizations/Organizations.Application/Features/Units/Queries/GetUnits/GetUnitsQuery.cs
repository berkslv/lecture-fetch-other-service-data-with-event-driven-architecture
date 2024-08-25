using MediatR;
using Microsoft.EntityFrameworkCore;
using Organizations.Application.Interfaces;

namespace Organizations.Application.Features.Units.Queries.GetUnits;

public record GetUnitsQuery : IRequest<List<GetUnitsQueryResponse>>;


public class GetUnitsQueryHandler : IRequestHandler<GetUnitsQuery, List<GetUnitsQueryResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetUnitsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<GetUnitsQueryResponse>> Handle(GetUnitsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Units
            .Select(x => new GetUnitsQueryResponse
            {
                Id = x.Id,
                Name = x.Name,
                Created = x.Created,
                Modified = x.Modified,
            })
            .ToListAsync(cancellationToken);
    }
}
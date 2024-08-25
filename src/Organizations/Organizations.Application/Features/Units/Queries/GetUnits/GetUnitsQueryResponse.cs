namespace Organizations.Application.Features.Units.Queries.GetUnits;

public record GetUnitsQueryResponse
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;

    public DateTime Created { get; set; }

    public DateTime? Modified { get; set; }

}
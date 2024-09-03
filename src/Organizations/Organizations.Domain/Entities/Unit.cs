using Organizations.Domain.Entities.Base;

namespace Organizations.Domain.Entities;

public class Unit : DispatchableEntity, IEntity
{
    public string Name { get; set; } = string.Empty;
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? Modified { get; set; }
    public string? ModifiedBy { get; set; }
}
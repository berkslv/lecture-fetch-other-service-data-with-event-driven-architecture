using Products.Domain.Entities.Base;

namespace Products.Domain.Entities;

public class Product : DispatchableEntity, IEntity
{
    public string Name { get; set; } = string.Empty;
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? Modified { get; set; }
    public string? ModifiedBy { get; set; }
}
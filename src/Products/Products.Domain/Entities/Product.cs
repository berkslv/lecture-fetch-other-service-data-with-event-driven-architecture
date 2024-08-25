using Products.Domain.Entities.Base;

namespace Products.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}
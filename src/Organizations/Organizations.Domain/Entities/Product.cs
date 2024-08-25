using Organizations.Domain.Entities.Base;

namespace Organizations.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}
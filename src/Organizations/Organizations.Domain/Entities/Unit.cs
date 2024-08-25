using Organizations.Domain.Entities.Base;

namespace Organizations.Domain.Entities;

public class Unit : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}
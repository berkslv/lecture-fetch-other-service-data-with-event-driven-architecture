namespace Shared.Contracts;

public record ProductCreatedEvent
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
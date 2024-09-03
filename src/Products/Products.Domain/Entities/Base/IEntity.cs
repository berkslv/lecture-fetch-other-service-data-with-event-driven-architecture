namespace Products.Domain.Entities.Base;

public interface IEntity
{
    Guid Id { get; set; }

    DateTime Created { get; set; }

    string CreatedBy { get; set; }

    DateTime? Modified { get; set; }

    string? ModifiedBy { get; set; }
}

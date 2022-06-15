namespace MicroServices.Playground.ServiceTwo.Api.Domain.Models;

public abstract class BaseEntity
{
    protected BaseEntity(Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        CreatedDateTime = DateTime.UtcNow;
    }

    public Guid Id { get; init; }
    public DateTime CreatedDateTime { get; init; }
    public DateTime? DeletedDateTime { get; private set; }

    public void Delete()
    {
        DeletedDateTime = DateTime.UtcNow;
    }
}

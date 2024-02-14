namespace LeadSync.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; private init; }

    protected readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

    protected Entity(Guid id)
    {
      Id = id;
    }

    public List<IDomainEvent> PopDomainEvents()
    {
      var copy = _domainEvents.ToList();
      _domainEvents.Clear();

      return copy;
    }

    protected Entity() { }
}
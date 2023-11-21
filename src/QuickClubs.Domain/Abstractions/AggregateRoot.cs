namespace QuickClubs.Domain.Abstractions;

public abstract class AggregateRoot<TEntityId> : Entity<TEntityId>
{
    protected AggregateRoot(TEntityId id) : base(id)
    {
    }

#pragma warning disable CS8618
    protected AggregateRoot()
    {
    }
#pragma warning restore CS8618
}

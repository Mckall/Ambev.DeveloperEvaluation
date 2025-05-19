namespace Ambev.DeveloperEvaluation.Domain.Events.Sales
{
    public abstract class DomainEvent
    {
        public DateTime OccurredOn { get; protected set; } = DateTime.UtcNow;
    }
}

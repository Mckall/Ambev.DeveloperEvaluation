namespace Ambev.DeveloperEvaluation.Domain.Events.Sales;

public interface IHasDomainEvents
{
    List<DomainEvent> DomainEvents { get; }
}

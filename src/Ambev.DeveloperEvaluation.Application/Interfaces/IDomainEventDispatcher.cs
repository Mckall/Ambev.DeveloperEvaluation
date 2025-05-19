using Ambev.DeveloperEvaluation.Domain.Events.Sales;

namespace Ambev.DeveloperEvaluation.Application.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(DomainEvent domainEvent);
}

using Ambev.DeveloperEvaluation.Domain.Events.Sales;

namespace Ambev.DeveloperEvaluation.Application.Interfaces;

public interface IDomainEventHandler<TEvent> where TEvent : DomainEvent
{
    Task Handle(TEvent domainEvent);
}

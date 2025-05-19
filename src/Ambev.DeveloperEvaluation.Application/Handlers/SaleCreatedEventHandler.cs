using Ambev.DeveloperEvaluation.Application.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;

namespace Ambev.DeveloperEvaluation.Application.Handlers;

public class SaleCreatedEventHandler : IDomainEventHandler<SaleCreatedEvent>
{
    public Task Handle(SaleCreatedEvent domainEvent)
    {
        Console.WriteLine($"Venda criada: {domainEvent.Sale.SaleNumber}");
        return Task.CompletedTask;
    }
}

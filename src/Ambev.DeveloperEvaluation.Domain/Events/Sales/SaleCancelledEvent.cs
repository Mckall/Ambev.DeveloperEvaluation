using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sales;

public class SaleCancelledEvent : DomainEvent
{
    public Sale Sale { get; }

    public SaleCancelledEvent(Sale sale)
    {
        Sale = sale;
    }
}

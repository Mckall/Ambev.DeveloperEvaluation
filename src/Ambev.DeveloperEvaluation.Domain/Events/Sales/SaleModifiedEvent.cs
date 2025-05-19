using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sales;

public class SaleModifiedEvent : DomainEvent
{
    public Sale Sale { get; }

    public SaleModifiedEvent(Sale sale)
    {
        Sale = sale;
    }
}

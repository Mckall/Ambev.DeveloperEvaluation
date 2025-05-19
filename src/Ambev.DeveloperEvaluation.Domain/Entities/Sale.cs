using Ambev.DeveloperEvaluation.Domain.Events.Sales;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale
{
    public Guid Id { get; private set; }
    public string SaleNumber { get; private set; } = string.Empty;
    public DateTime SaleDate { get; private set; }
    public Guid ClientId { get; private set; }
    public string ClientName { get; private set; } = string.Empty;
    public Guid BranchId { get; private set; }
    public string BranchName { get; private set; } = string.Empty;
    public List<SaleItem> Items { get; private set; } = [];
    public bool IsCancelled { get; private set; }
    public decimal TotalAmount => Items.Sum(i => i.TotalPrice);
    public List<DomainEvent> DomainEvents { get; } = new();

    private Sale() { }

    public Sale(string saleNumber, DateTime saleDate, Guid clientId, string clientName, Guid branchId, string branchName, List<SaleItem> items)
    {
        Id = Guid.NewGuid();
        SaleNumber = saleNumber;
        SaleDate = saleDate;
        ClientId = clientId;
        ClientName = clientName;
        BranchId = branchId;
        BranchName = branchName;
        Items = items;

        ValidateBusinessRules();

        LogEvent("SaleCreated");
    }

    public void ValidateBusinessRules()
    {
        foreach (var item in Items)
        {
            if (item.Quantity > 20)
                throw new ArgumentException("It is not allowed to sell more than 20 identical items.");

            item.ApplyDiscountByQuantity();
        }
    }

    public void CancelSale()
    {
        if (IsCancelled)
            throw new InvalidOperationException("Sale already canceled.");

        IsCancelled = true;
        LogEvent("SaleCancelled");
    }

    public void ModifySale(List<SaleItem> newItems)
    {
        Items = newItems;
        ValidateBusinessRules();
        LogEvent("SaleModified");
    }

    private void LogEvent(string eventName)
    {
        Console.WriteLine($"{eventName} - SaleNumber: {SaleNumber}, Date: {SaleDate}");
    }

    public void AddItem(SaleItem item)
    {
        Items.Add(item);
    }

    public void MarkAsCreated()
    {
        DomainEvents.Add(new SaleCreatedEvent(this));
    }

    public void MarkAsCancelled()
    {
        DomainEvents.Add(new SaleCancelledEvent(this));
    }

    public void MarkAsModified()
    {
        DomainEvents.Add(new SaleModifiedEvent(this));
    }
}
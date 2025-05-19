namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem
{
    public Guid Id { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; } = default!;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal DiscountPercentage { get; private set; }
    public decimal FinalPrice => TotalPrice * (1 - DiscountPercentage);
    public decimal Discount { get; set; }

    private SaleItem() { }

    public SaleItem(string productId, string productName, int quantity, decimal unitPrice)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = 0;
    }

    public void ApplyDiscountByQuantity()
    {
        if (Quantity < 4)
        {
            Discount = 0;
        }
        else if (Quantity >= 4 && Quantity < 10)
        {
            Discount = 0.10m;
        }
        else if (Quantity >= 10 && Quantity <= 20)
        {
            Discount = 0.20m;
        }

        TotalPrice = Quantity * UnitPrice * (1 - Discount);
    }

    public void CancelItem()
    {
        Console.WriteLine($"ItemCancelled - Product: {ProductName}, Quantity: {Quantity}");
    }
}

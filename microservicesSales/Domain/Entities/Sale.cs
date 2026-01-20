public class Sale {
    public int Id { get; private set; }
    public DateTime Date { get; private set; }
    public List<SaleItem> Items { get; private set; }
    public decimal TotalAmount { get; private set; }
    private Sale() { }
    public Sale(DateTime date, List<SaleItem> items)
    {
        if (items == null || items.Count == 0) throw new ArgumentException("La venta debe tener al menos un item.", nameof(items));
        Date = date;
        Items = items;
        TotalAmount = CalculateTotalAmount();
    }
    private decimal CalculateTotalAmount()
    {
        decimal total = 0;
        foreach (var item in Items)
        {
            total += item.Subtotal;
        }
        return total;
    }
}

public class SaleItem {
    public int Id { get; private set; }
    public int ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Subtotal => Quantity * UnitPrice;
    private SaleItem() { }
    public SaleItem(int productId, int quantity, decimal unitPrice)
    {
        if (quantity <= 0) throw new ArgumentException("La cantidad debe ser mayor a 0", nameof(quantity));
        if (unitPrice < 0) throw new ArgumentException("El precio unitario no puede ser negativo.", nameof(unitPrice));
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}   
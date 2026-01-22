

using microservicesSales.Domain;

public class Sale {
    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTime Date { get; private set; } = DateTime.UtcNow;
    public List<SaleItem> Items { get; private set; } = new();
    public decimal TotalAmount  => Items.Sum(item => item.Subtotal);
    private Sale() { }
    public static Sale Create(IEnumerable<(Product product, int quantity)> lines)
    {
        var sale = new Sale();

        foreach (var (product, qty) in lines)
        {
            sale.addItem(product, qty);
        }
        
        if(sale.Items.Count == 0) throw new DomainException("La venta debe tener al menos un item.");

        return sale;


    }

    private void addItem(Product product, int quantity)
    {
        if (quantity <=0 ) throw new DomainException("La cantidad no puede ser menor a 0");
        Items.Add(new SaleItem(product.Id,product.Name, quantity, product.Price));
    }


    
}

public class SaleItem {
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid SaleId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Subtotal => Quantity * UnitPrice;
    private SaleItem() { }
    public SaleItem(Guid productId, string productName, int quantity, decimal unitPrice)
    {
        if (quantity <= 0) throw new DomainException("La cantidad debe ser mayor a 0");
        if (unitPrice < 0) throw new DomainException("El precio unitario no puede ser negativo.");
        if (string.IsNullOrEmpty(productName)) throw new DomainException("El nombre del producto ir vacio.");


        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        ProductName = productName;
    }
}   
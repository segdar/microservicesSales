public class Product
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }


    public byte[] RowVersion { get; private set; } = Array.Empty<byte>();
    private Product() { }

    public Product(string name, decimal price, int stock)
    {

        if(string.IsNullOrEmpty(name)) throw new ArgumentException("Nombre Requerido.", nameof(name));
        if(price < 0) throw new ArgumentException("El precio debe ser positivo.", nameof(price));
        if(stock < 0) throw new ArgumentException("El stock no puede ser negativo.", nameof(stock));

        Name = name;
        Price = price;
        Stock = stock;
    }

    public void UpdateStock(int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("La cantidad debe ser mayor a 0");
        Stock += quantity;
    }

    public void ReducerStock(int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("La cantidad debe ser mayor a 0");
        if (Stock < quantity) throw new InvalidOperationException("Stock insuficiente.");
        Stock -= quantity;
    }

    public void updateProducto(string newName)
    {
        if (string.IsNullOrEmpty(newName)) throw new ArgumentException("El nombre no puede estar vacio.", nameof(newName));
        Name = newName;

    }
    public void changePrice(decimal newPrice)
    {
        if (newPrice < 0) throw new ArgumentException("El precio debe ser positivo.", nameof(newPrice));
        Price = newPrice;
    }

}
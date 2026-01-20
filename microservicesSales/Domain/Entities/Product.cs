public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }



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

}
namespace microservicesSales.Application.DTOs
{
    public record CreateProductRequest(string Name, decimal Price, int InitialStock);
    public record UpdateProductRequest(string Name, decimal Price);
    public record ProductResponse(Guid Id,string Name,decimal Price, int Stock);
  

    public record AdjustStockRequest(int Quantity);

    public record CreateSaleRequest(List<CreateSaleLine> Items);
    public record CreateSaleLine(Guid ProductId, int Quantity);

    public record SaleResponse(Guid Id, DateTime date, decimal Total, List<SaleItemResponse> Items);
    public record SaleItemResponse(Guid ProductId, string ProductName, decimal UnitPrice, int Quantity, decimal Subtotal);
}

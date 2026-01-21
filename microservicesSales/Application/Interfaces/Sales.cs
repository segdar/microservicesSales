namespace microservicesSales.Application.Interfaces
{
    public interface ISaleRepository
    {
        Task<Sale?> GetByIdAsync(Guid id, CancellationToken ct);
        Task AddAsync(Sale sale, CancellationToken ct);
    }

    public interface ISalesNotifier
    {
        Task SaleCreatedAsync(Sale sale, CancellationToken ct);
        Task InventoryChangedAsync(Product product, CancellationToken ct);
    }

}

namespace microservicesSales.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<List<Product>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct);
        Task<List<Product>> GetAllAsync(CancellationToken ct);
        Task AddAsync(Product product, CancellationToken ct);
        Task Update(Product product);
        Task Delete(Product product);
    }

}

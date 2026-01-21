

using microservicesSales.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db; 
    public ProductRepository(AppDbContext db) => _db = db;

    public Task<Product?> GetByIdAsync(Guid id, CancellationToken ct) =>
        _db.Products.FirstOrDefaultAsync(p => p.Id == id, ct);

    public Task<List<Product>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct) =>
        _db.Products.Where(p => ids.Contains(p.Id)).ToListAsync(ct);

    public Task AddAsync(Product product, CancellationToken ct) =>
        _db.Products.AddAsync(product, ct).AsTask();

    public Task Update(Product product)
    {
        _db.Products.Update(product);
        return Task.CompletedTask;
    }

    public Task Delete(Product product){
        _db.Products.Remove(product);
        return Task.CompletedTask;
    }

    public Task<List<Product>> GetAllAsync(CancellationToken ct)
    {
        return _db.Products.ToListAsync(ct);
    }
}
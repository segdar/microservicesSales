using microservicesSales.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace microservicesSales.Infrastructure.Repository
{
    public class SalesRepository : ISaleRepository
    {

        private readonly AppDbContext _db;
        public SalesRepository(AppDbContext db) => _db = db;

        public Task AddAsync(Sale sale, CancellationToken ct) =>
            _db.Sales.AddAsync(sale, ct).AsTask();

        public Task<Sale?> GetByIdAsync(Guid id, CancellationToken ct) =>
            _db.Sales
               .Include(s => s.Items) 
               .FirstOrDefaultAsync(s => s.Id == id, ct);
    }
}

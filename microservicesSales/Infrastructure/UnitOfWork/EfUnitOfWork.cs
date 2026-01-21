using microservicesSales.Application.Interfaces;

namespace microservicesSales.Infrastructure.UnitOfWork
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public EfUnitOfWork(AppDbContext db)
        {
            _db = db;
        }
        public async Task ExecuteInTransactionAsync(Func<CancellationToken, Task> action, CancellationToken ct)
        {
            await using var tx = await _db.Database.BeginTransactionAsync(ct);
            try
            {
                await action(ct);
                await tx.CommitAsync(ct);
            }
            catch
            {
                await tx.RollbackAsync(ct);
                throw;
            }
        }

        public Task<int> SaveChangesAsync(CancellationToken ct)
        {
          return  _db.SaveChangesAsync(ct);
        }
    }
}

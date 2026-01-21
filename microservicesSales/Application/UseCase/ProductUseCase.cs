using microservicesSales.Application.DTOs;
using microservicesSales.Application.Interfaces;

namespace microservicesSales.Application.UseCase
{
    public class ProductUseCase
    {
        private readonly IUnitOfWork _uow;
        private readonly IProductRepository _products;
        private readonly ISalesNotifier _notifier;

        public ProductUseCase(IUnitOfWork uow, IProductRepository products, ISalesNotifier notifier)
        {
            _uow = uow;
            _products = products;
            _notifier = notifier;
        }

        public async Task<List<ProductResponse>> GetAllProductsAsync (CancellationToken ct)
        {
            var products = await _products.GetAllAsync(ct);
            return products.Select(product => new ProductResponse(
                product.Id,
                product.Name,
                product.Price,
                product.Stock
            )).ToList();
        }
        public async Task<Guid> CreateProductAsync(CreateProductRequest req, CancellationToken ctx)
        {
            var product = new Product(req.Name, req.Price, req.InitialStock);
            await _products.AddAsync(product, ctx);
            await _uow.SaveChangesAsync(ctx);

            await _notifier.InventoryChangedAsync(product, ctx);
            return product.Id;  

        }


        public  async Task<ProductResponse?> GetProductById (Guid id, CancellationToken ct)
        {
            var product = await _products.GetByIdAsync(id, ct);
            if (product is null) return null;

           return new ProductResponse(
               product.Id,
               product.Name,
               product.Price,
               product.Stock
            );
        }

        public async Task<Guid> UpdateProductAsync(Guid id, UpdateProductRequest req, CancellationToken ct)
        {
            var product = await _products.GetByIdAsync(id, ct);
            if (product is null) throw new ArgumentException("El producto no existe.");

            product.updateProducto(req.Name);
            if(product.Price != req.Price)
            {
                product.changePrice(req.Price);
            }
    

            await _products.Update(product);
            await _uow.SaveChangesAsync(ct);
            return product.Id;  
        }

        public async Task<Guid>  DeleteProductAsync (Guid id, CancellationToken ct)
        {
            var product = await _products.GetByIdAsync(id, ct);
            if (product is null) throw new ArgumentException("El producto no existe.");
            await _products.Delete(product);
            await _uow.SaveChangesAsync(ct);
            return product.Id;
        }

        public async Task IncreaseStockAsync(Guid id, int quantity, CancellationToken ct)
        {
            var product = await _products.GetByIdAsync(id, ct);
            if (product is null) throw new ArgumentException("El producto no existe.");
            product.UpdateStock(quantity);
            await _uow.SaveChangesAsync(ct);

            await _notifier.InventoryChangedAsync(product, ct);
        }

        public async Task ReduceStockAsync(Guid id, int quantity, CancellationToken ct)
        {
            var product = await _products.GetByIdAsync(id, ct);
            if (product is null) throw new ArgumentException("El producto no existe.");
            product.ReducerStock(quantity);
            await _uow.SaveChangesAsync(ct);

            await _notifier.InventoryChangedAsync(product, ct);

        }



    }
}

using microservicesSales.Application.DTOs;
using microservicesSales.Application.Interfaces;

namespace microservicesSales.Application.UseCase
{
    public class SalesUseCase
    {
        private readonly ISaleRepository _sales;
        private readonly IUnitOfWork _uow;
        private readonly ISalesNotifier _notifier;
        private readonly IProductRepository _products;

        public SalesUseCase(ISaleRepository sales, IUnitOfWork uow, ISalesNotifier notifier, IProductRepository products)
        {
            _sales = sales;
            _uow = uow;
            _notifier = notifier;
            _products = products;
        }

        public async Task<Guid> CreateSale(CreateSaleRequest req, CancellationToken ct)
        {
            if(req.Items == null || !req.Items.Any())
            {
                throw new ArgumentException("La venta debe tener detalle");
            }

            Guid saleId = Guid.Empty;
            Sale? saleCreated = null;

            await _uow.ExecuteInTransactionAsync(async innerCt =>
            {
               var productID = req.Items.Select(i => i.ProductId).Distinct().ToList();  
               var products = await _products.GetByIdsAsync(productID,innerCt);
                
                if(products.Count != productID.Count)
                {
                    throw new ArgumentException("Uno o mas productos no existen.");
                }

                var LstIdsProducts = products.ToDictionary(product => product.Id, p => p);
                
                foreach(var item in req.Items)
                {
                    var product = LstIdsProducts[item.ProductId];
                    product.ReducerStock(item.Quantity);
                }

                var lines = req.Items.Select(l => (product: LstIdsProducts[l.ProductId], quantity: l.Quantity));
                var sale = Sale.Create(lines);

                await _sales.AddAsync(sale,innerCt);
                await _uow.SaveChangesAsync(innerCt);

                saleId = sale.Id;
                saleCreated = sale;


            }, ct);

            if(saleCreated is not null)
            {
                await _notifier.SaleCreatedAsync(saleCreated, ct);
               
            }
            return saleId;
        }

        public async Task<SaleResponse?> GetSaleByIdAsync(Guid id, CancellationToken ct)
        {
           var sale = await _sales.GetByIdAsync(id, ct);
            if(sale is null) return null;   

           return  new SaleResponse
            (
                sale.Id,
                sale.Date,
                sale.TotalAmount,
                sale.Items.Select(item => new SaleItemResponse
                (
                    item.ProductId,
                    item.ProductName,
                    item.UnitPrice,
                    item.Quantity,
                    item.Subtotal
                )).ToList()
            );
                  
        }





    }
}

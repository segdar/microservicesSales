using microservicesSales.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace microservicesSales.Infrastructure.Hubs
{
    public class SalesNotifier : ISalesNotifier
    {

        private readonly IHubContext<SalesHub> _hub;
        public SalesNotifier(IHubContext<SalesHub> hub)
        {
            _hub = hub;
        }

        public Task InventoryChangedAsync(Product product, CancellationToken ct)
        {
            return _hub.Clients.All.SendAsync("inventoryChanged", new
            {
                productId = product.Id,
                name = product.Name,
                stock = product.Stock
            }, ct);
        }

        public Task SaleCreatedAsync(Sale sale, CancellationToken ct)
        {
           return  _hub.Clients.All.SendAsync("saleCreated", new
            {
                saleId = sale.Id,
                createdAtUtc = sale.Date,
                total = sale.TotalAmount
            }, ct);
        }
    }
}

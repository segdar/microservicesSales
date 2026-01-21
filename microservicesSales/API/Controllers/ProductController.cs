using microservicesSales.Application.DTOs;
using microservicesSales.Application.UseCase;
using Microsoft.AspNetCore.Mvc;

namespace microservicesSales.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly ProductUseCase _productUseCase;

        public ProductController(ProductUseCase productUseCase)
        {
            _productUseCase = productUseCase;
        }


        [HttpPost("CreateProduct")]
        public async Task<ProductResponse?> CreateProduct([FromBody] CreateProductRequest req, CancellationToken ct)
        {

            var saleId = await _productUseCase.CreateProductAsync(req, ct);
            return await _productUseCase.GetProductById(saleId, ct);
        }

        [HttpGet("GetAllProducts")]
        public async Task<List<ProductResponse>> GetAllProducts(CancellationToken ct)
        {
            return await _productUseCase.GetAllProductsAsync(ct);
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<ProductResponse?> GetProductById([FromRoute] Guid id, CancellationToken ct)
        {
            return await _productUseCase.GetProductById(id, ct);


        }

        [HttpPatch("UpdateProduct/{id}")]  
        public async Task<ProductResponse?> UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductRequest req, CancellationToken ct)
        {
            var productId = await _productUseCase.UpdateProductAsync(id, req, ct);
            return await _productUseCase.GetProductById(productId, ct);
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<Guid> DeleteProduct([FromRoute] Guid id, CancellationToken ct)
        {
            return await _productUseCase.DeleteProductAsync(id, ct);
        }

        [HttpPatch("AdjustStock/{id}")]    
        public async Task<ActionResult> AdjustStock([FromRoute] Guid id, [FromBody] AdjustStockRequest req, CancellationToken ct)
        {
            await _productUseCase.IncreaseStockAsync(id, req.Quantity, ct);
            return NoContent();
        }

        [HttpPatch("ReduceStock/{id}")]
        public async Task<ActionResult> ReduceStock([FromRoute] Guid id, [FromBody] AdjustStockRequest req, CancellationToken ct)
        {
            await _productUseCase.ReduceStockAsync(id, req.Quantity, ct);
            return NoContent();
        }



    }


}

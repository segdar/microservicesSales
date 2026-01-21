using microservicesSales.Application.DTOs;
using microservicesSales.Application.UseCase;
using Microsoft.AspNetCore.Mvc;

namespace microservicesSales.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly SalesUseCase _salesUseCase;

        public SalesController(SalesUseCase salesUseCase)
        {
            _salesUseCase = salesUseCase;
        }

        [HttpPost("CreateSale")]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest req, CancellationToken ct) {

            var saleId = await _salesUseCase.CreateSale(req, ct);
            return Created($"/sales/{saleId}", new { id = saleId });
        }

        [HttpGet("GetSale")]
        public async Task<IActionResult> GetSaleById([FromRoute] Guid id, CancellationToken ct) {
            var sale = await _salesUseCase.GetSaleByIdAsync(id, ct);
            if(sale is null)
            {
                return NotFound();
            }
            return Ok(sale);
        }

    }
}

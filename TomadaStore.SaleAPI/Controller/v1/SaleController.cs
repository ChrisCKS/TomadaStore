using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomadaStore.SaleAPI.Model;
using TomadaStore.SaleAPI.Service.v1.Interface;

namespace TomadaStore.SaleAPI.Controller.v1
{
    [Route("api/v1[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ILogger<SaleController> _logger;
        private readonly ISaleService _saleService;

        public SaleController(ILogger<SaleController> logger, ISaleService saleService)
        {
            _logger = logger;
            _saleService = saleService;
        }

        [HttpPost("customer/{idCustomer}")]
        public async Task<IActionResult> CreateSaleAsync(int idCustomer, [FromBody] SaleRequestDTO saleDTO)
        {
            _logger.LogInformation("Creating a new sale.");
    
            await  _saleService.CreateSaleAsync(idCustomer, saleDTO);
            return Ok();
        }
    }
}

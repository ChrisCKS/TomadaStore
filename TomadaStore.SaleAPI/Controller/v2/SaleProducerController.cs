using Microsoft.AspNetCore.Mvc;
using TomadaStore.SaleAPI.Model;
using TomadaStore.SaleAPI.Service.v2;
using TomadaStore.SaleAPI.Service.v2.Interface;

namespace TomadaStore.SaleAPI.Controller.v2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class SaleProducerController : ControllerBase
    {
        public readonly ISaleProduceService _producer;
        

        public SaleProducerController(ISaleProduceService producer)
        {
            _producer = producer;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale ([FromQuery] int idCustomer, [FromBody] SaleRequestDTO request)
        {
            var sale = await _producer.CreateSaleAsync(idCustomer, request.ProductsIds);

            await _producer.SaleProducerSendAsync(sale);

            return Accepted("Sale sent to queue");
        }
    }
}

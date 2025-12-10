using Microsoft.AspNetCore.Mvc;
using TomadaStore.Models.Models;
using TomadaStore.SaleAPI.Model;

namespace TomadaStore.SaleAPI.Service.v2.Interface
{
    public interface ISaleProduceService
    {
        Task<Sale> CreateSaleAsync(int idCostumer, List<string> idsProduct);
        Task SaleProducerSendAsync(Sale sale);

    }
}

using TomadaStore.Models.Models;
using TomadaStore.SaleAPI.Model;

namespace TomadaStore.SaleAPI.Service.Interface
{
    public interface ISaleService
    {
        Task CreateSaleAsync(int idCustomer, string idProduct, SaleRequestDTO saleDTO);

    }
}

using TomadaStore.Models.Models;
using TomadaStore.SaleAPI.Model;

namespace TomadaStore.SaleAPI.Service.v1.Interface
{
    public interface ISaleService
    {
        Task CreateSaleAsync(int idCustome, SaleRequestDTO saleDTO);    /**/

    }
}

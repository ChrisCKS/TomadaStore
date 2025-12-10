using TomadaStore.Models.Models;

namespace PaymentValidAPI.Service.Interface
{
    public interface IPaymentService
    {
        Task ProcessPaymentAsync();
        Task ValidatePaymentAsync(Sale sale);
    }
}

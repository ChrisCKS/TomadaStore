using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadaStore.CustomerAPI.Service.Interface
{
    public interface ICustomerService
    {
        Task InsertCustomerAsync(CustomerRequestDTO customer);
        Task<List<CustomerResponseDTO>> GetAllCustomersAsync();
        Task<CustomerResponseDTO> GetCustomerByIdAsync(int id);
        Task UpdateCustomerStatusAsync(int id);
    }
}
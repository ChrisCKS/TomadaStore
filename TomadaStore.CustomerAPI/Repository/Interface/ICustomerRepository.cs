using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadaStore.CustomerAPI.Repository.Interface
{
    public interface ICustomerRepository
    {
        Task InsertCustomerAsync(CustomerRequestDTO customer);
        Task<List<CustomerResponseDTO>> GetAllCustomersAsync();
        Task<CustomerResponseDTO> GetCustomerByIdAsync(int id);
        Task UpdateCustomerStatusAsync(int id);
    }
}

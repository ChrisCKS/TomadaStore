using TomadaStore.CustomerAPI.Repository.Interface;
using TomadaStore.CustomerAPI.Service.Interface;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadaStore.CustomerAPI.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ILogger<CustomerService> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }
        public async Task<List<CustomerResponseDTO>> GetAllCustomersAsync()
        {
            try
            {
                return await _customerRepository.GetAllCustomersAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<CustomerResponseDTO> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerByIdAsync(id);

                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving customer with ID {id}");
                throw;
            }
        }

        public async Task InsertCustomerAsync(CustomerRequestDTO customer)
        {
            try
            {
                await _customerRepository.InsertCustomerAsync(customer);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateCustomerStatusAsync(int id)
        {
            try
            {
                await _customerRepository.UpdateCustomerStatusAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomadaStore.CustomerAPI.Service;
using TomadaStore.CustomerAPI.Service.Interface;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadaStore.CustomerAPI.Controller.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomerAsync([FromBody] CustomerRequestDTO customer)
        {
            try
            {
                _logger.LogInformation("Creating a new customer.");

                await _customerService.InsertCustomerAsync(customer);

                return Created();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while creating a new customer. " + e.Message);
                return Problem(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerResponseDTO>>> GetAllCustomersAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all customers."); //mostra no terminal
                var customers = await _customerService.GetAllCustomersAsync();
                return Ok(customers);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while retrieving all customers. " + e.Message);
                return Problem(e.StackTrace);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponseDTO>> GetCustomerByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Retrieving customer with ID {id}.");

                var customer = await _customerService.GetCustomerByIdAsync(id);

                return Ok(customer);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error occurred while retrieving customer with ID {id}. " + e.Message);
                return Problem(e.StackTrace);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> UpdateCustomerStatusAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Updating status for customer with ID {id}.");

                await _customerService.UpdateCustomerStatusAsync(id);

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error occurred while updating status for customer with ID {id}. " + e.Message);

                return Problem(e.StackTrace);
            }
        }
    }
}

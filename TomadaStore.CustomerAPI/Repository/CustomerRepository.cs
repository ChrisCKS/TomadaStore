using Dapper;
using Microsoft.Data.SqlClient;
using TomadaStore.CustomerAPI.Data;
using TomadaStore.CustomerAPI.Repository.Interface;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadaStore.CustomerAPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger<CustomerRepository> _logger;   
        private readonly SqlConnection _connection;

        public CustomerRepository(ILogger<CustomerRepository> logger, ConnectionDB connection)
        {
            _logger = logger;
            _connection = connection.GetConnection();
        }

        public async Task<List<CustomerResponseDTO>> GetAllCustomersAsync()
        {
            try 
            {
                var sqlSelect = "SELECT Id, FirstName, LastName, Email, PhoneNumber FROM Customers";

                var customers = await _connection.QueryAsync<CustomerResponseDTO>(sqlSelect);

                return customers.ToList();
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError($"SQL Error occurred while retrieving all customers: {sqlEx.Message}");
                                            
                throw new Exception(sqlEx.StackTrace); //traz todo o contexto do erro
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all customers. " + ex.Message);
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task<CustomerResponseDTO> GetCustomerByIdAsync(int id)
        {
            try 
            {
                var sqlSelect = "SELECT Id, FirstName, LastName, Email, PhoneNumber FROM Customers WHERE Id = @Id";

                var customer = await _connection.QueryFirstOrDefaultAsync<CustomerResponseDTO>(sqlSelect, new { Id = id });

                return customer;
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError($"SQL Error occurred while retrieving customer by ID: {sqlEx.Message}");
                                            
                throw new Exception(sqlEx.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving customer by ID. " + ex.Message);
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task InsertCustomerAsync(CustomerRequestDTO customer)
        {
            try
            {
                var insertSql = "INSERT INTO Customers (FirstName, LastName,  Email, PhoneNumber, Status) " +
                                "VALUES (@FirstName, @LastName, @Email, @PhoneNumber, 1)";

                await _connection.ExecuteAsync(insertSql, new { customer.FirstName, customer.LastName, customer.Email, customer.PhoneNumber});

            }
            catch (SqlException sqlEx)
            {
                _logger.LogError($"SQL Error occurred while inserting a new customer: {sqlEx.Message}");
                                            
                throw new Exception(sqlEx.StackTrace); //traz todo o contexto do erro
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while inserting a new customer. " + ex.Message);
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task UpdateCustomerStatusAsync(int id) 
        {
            try 
            {
                var Sql = "UPDATE Customers SET Status = 0 WHERE Id = @Id";
                await _connection.ExecuteAsync(Sql, new { Id = id });
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError($"SQL Error occurred while updating customer status: {sqlEx.Message}");
                                            
                throw new Exception(sqlEx.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating customer status. " + ex.Message);
                throw new Exception(ex.StackTrace);
            }
        }
    }
}

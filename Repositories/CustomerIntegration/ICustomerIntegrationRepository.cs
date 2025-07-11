using tripath.Models;

public interface ICustomerIntegrationRepository
{
    Task<List<CustomerIntegration>> GetAllAsync();
    Task<CustomerIntegration?> GetByIdAsync(string id);
    Task<List<CustomerIntegration>> GetByCustomerIdAsync(string customerId);
    Task<CustomerIntegration> CreateAsync(CustomerIntegration integration);
    Task UpdateAsync(string id, CustomerIntegration integration);
    Task<bool> DeleteAsync(string id);
}

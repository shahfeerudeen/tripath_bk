using tripath.Models;

public interface ICustomerCarrierRepository
{
    Task<CustomerCarrier> CreateAsync(CustomerCarrier carrier);

    Task UpdateAsync(string id, CustomerCarrier carrier);
    Task<List<CustomerCarrier>> GetAllAsync();
    Task<List<CustomerCarrier>> GetByCustomerIdAsync(string customerId);
    Task<CustomerCarrier> GetByIdAsync(string id);
    Task<bool> SoftDeleteAsync(string id);
}

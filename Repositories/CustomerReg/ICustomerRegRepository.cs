using tripath.Models;

public interface ICustomerRegRepository
{
    Task<CustomerReg> CreateAsync(CustomerReg customerReg);
    Task UpdateAsync(string id, CustomerReg updatedCustomerReg);
    Task<CustomerReg> UpsertAsync(CustomerReg customerReg);
    Task<CustomerReg> GetByIdAsync(string id);
    Task<List<CustomerReg>> GetByCustomerIdAsync(string customerId);
    Task<List<CustomerReg>> GetAllAsync();
    Task<bool> DeleteAsync(string id);
    Task<CustomerReg?> GetByCustomerIdAndRegIdAsync(string customerId, string customerRegistrationId);

}

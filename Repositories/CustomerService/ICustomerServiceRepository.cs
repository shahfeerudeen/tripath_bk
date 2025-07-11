using tripath.Models;

namespace tripath.Repositories
{
   public interface ICustomerServiceRepository
{
    Task<CustomerServices> CreateAsync(CustomerServices customer);
    Task UpdateAsync(string id, CustomerServices updatedService);
    Task<CustomerServices> GetByIdAsync(string id);
    Task<CustomerServices> GetByCustomerIdAsync(string customerId);
    Task<CustomerServices> GetByCustomerIdAndServiceIdAsync(string customerId, string serviceId);
    Task<IEnumerable<CustomerServices>> GetAllAsync();
    Task<bool> DeletedAsync(string id);
}

}

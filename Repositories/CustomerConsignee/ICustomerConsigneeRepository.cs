using tripath.Models;

namespace tripath.Repositories
{
    public interface ICustomerConsigneeRepository
    {
        Task<CustomerConsignee> CreateAsync(CustomerConsignee consignee);
        Task UpdateAsync(string id, CustomerConsignee updatedConsignee);
        Task<List<CustomerConsignee>> GetAllAsync();
        Task<CustomerConsignee> GetByIdAsync(string consigneeId);
        Task<List<CustomerConsignee>> GetByCustomerIdAsync(string customerId);
        Task<bool> DeleteAsync(string consigneeId);
        Task<CustomerConsignee?> GetByCustomerIdAndConsigneeIdAsync(string customerId, string consigneeId);

    }
}

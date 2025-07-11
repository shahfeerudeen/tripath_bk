using tripath.Models;

namespace tripath.Repositories
{
    public interface ICustomerAddressRepository
    {
        Task<CustomerAddress> CreateAsync(CustomerAddress address);
        Task<CustomerAddress?> GetByIdAsync(string addressId);
        Task<IEnumerable<CustomerAddress>> GetAllByCustomerIdAsync(string customerId);
        Task<bool> DeleteAsync(string addressId);
        Task<CustomerAddress?> UpdateAsync(string addressId, CustomerAddress updated);
    }
}

using tripath.Models;

namespace tripath.Repositories
{
    public interface ICustomerDetailAddressRepository
    {
        Task<CustomerDetailAddress> CreateAsync(CustomerDetailAddress customer);
        Task UpdateAsync(string id, CustomerDetailAddress updatedCustomer);

        Task<List<CustomerDetailAddress>> GetByCustomerIdAsync(string customerId);

        Task<List<CustomerDetailAddress>> GetAllAsync();
        Task<CustomerDetailAddress> GetByIdAsync(string customerDetailAddressId);

        Task<bool> DeleteAsync(string id);

        Task<CustomerDetailAddress> GetByCustomerIdAndAddressIdAsync(string customerId, string customerDetailAddressId);

    }
}

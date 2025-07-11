using tripath.Models;

namespace tripath.Repositories
{
    public interface ICustomerShipperRepository
    {
        Task<CustomerShipper> CreateAsync(CustomerShipper shipper);
        Task UpdateAsync(string id, CustomerShipper updatedShipper);
        Task<CustomerShipper?> GetByIdAsync(string id);
        Task<CustomerShipper?> GetByCustomerIdAndShipperIdAsync(string customerId, string shipperId);
        Task<List<CustomerShipper>> GetByCustomerIdAsync(string customerId);
        Task<List<CustomerShipper>> GetAllAsync();
        Task<bool> DeleteAsync(string id);
    }
}

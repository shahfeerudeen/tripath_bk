using tripath.Models;

namespace tripath.Repositories;

public interface ICustomerMasterRepository
{
    Task<CustomerMaster> CreateAsync(CustomerMaster master);
    Task<CustomerWithAddressResponse?> GetByIdWithAddressAsync(string customerId);
    Task<IEnumerable<CustomerWithAddressResponse>> GetAllAsync();

    Task<bool> DeleteAsync(string customerId);
    Task<CustomerMaster?> PartialUpdateAsync(
        string customerId,
        CustomerMasterUpdateModel updatedCustomer,
        string updatedBy
    );
    Task<bool> ExistsAsync(string customerId);
}

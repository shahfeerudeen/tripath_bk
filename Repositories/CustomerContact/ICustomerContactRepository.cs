using tripath.Models;

namespace tripath.Repositories
{
    public interface ICustomerContactRepository
    {
        Task<CustomerContact> CreateAsync(CustomerContact contact);
        Task<CustomerContact> GetByIdAsync(string contactId);
        Task UpdateAsync(string id, CustomerContact updatedContact);
        Task<List<CustomerContact>> GetAllAsync();

         Task<List<CustomerContact>> GetByCustomerIdAsync(string customerId);

         Task<bool> DeleteAsync(string contactId);
       
    }
}

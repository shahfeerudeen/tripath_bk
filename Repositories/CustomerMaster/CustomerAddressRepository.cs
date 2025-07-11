using Data;
using MongoDB.Driver;
using tripath.Models;

namespace tripath.Repositories
{
    public class CustomerAddressRepository : ICustomerAddressRepository
    {
        private readonly IMongoCollection<CustomerAddress> _collection;

        public CustomerAddressRepository(DbContextClass context)
        {
            _collection = context.Database.GetCollection<CustomerAddress>("CustomerAddress");
        }

        public async Task<CustomerAddress> CreateAsync(CustomerAddress address)
        {
            await _collection.InsertOneAsync(address);
            return address;
        }

        public async Task<CustomerAddress?> GetByIdAsync(string addressId)
        {
            return await _collection
                .Find(x => x.CustomerAddressId == addressId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CustomerAddress>> GetAllByCustomerIdAsync(string customerId)
        {
            return await _collection.Find(x => x.CustomerId == customerId).ToListAsync();
        }

        public async Task<bool> DeleteAsync(string addressId)
        {
            var result = await _collection.DeleteOneAsync(x => x.CustomerAddressId == addressId);
            return result.DeletedCount > 0;
        }

        public async Task<CustomerAddress?> UpdateAsync(string addressId, CustomerAddress updated)
        {
            updated.CustomerAddressUpdateDate = DateTime.UtcNow;

            var result = await _collection.ReplaceOneAsync(
                x => x.CustomerAddressId == addressId,
                updated
            );

            return result.IsAcknowledged && result.ModifiedCount > 0 ? updated : null;
        }
    }
}

using MongoDB.Bson;
using MongoDB.Driver;
using tripath.Models;

namespace tripath.Repositories
{
    public class CustomerDetailAddressRepository : ICustomerDetailAddressRepository
    {
        private readonly IMongoCollection<CustomerDetailAddress> _collection;

        public CustomerDetailAddressRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<CustomerDetailAddress>("CustomerDetailAddress");
        }

        public async Task<CustomerDetailAddress> CreateAsync(CustomerDetailAddress customer)
        {
            await _collection.InsertOneAsync(customer);
            return customer;
        }

        public async Task UpdateAsync(string id, CustomerDetailAddress updatedCustomer)
        {
            var filter = Builders<CustomerDetailAddress>.Filter.Eq("_id", new ObjectId(id));
            var updateBuilder = Builders<CustomerDetailAddress>.Update;
            var updates = new List<UpdateDefinition<CustomerDetailAddress>>();

            var properties = typeof(CustomerDetailAddress).GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(updatedCustomer);

                // Skip nulls and system fields
                if (value == null ||
                    prop.Name == "CustomerId" ||
                    prop.Name == "CustomerDetailAddressId" ||
                    prop.Name == "CustomerDetailAddressEntryDate" ||
                    prop.Name == "CustomerDetailAddressCreatedBy")
                    continue;

                // For strings, skip if empty
                if (prop.PropertyType == typeof(string) && string.IsNullOrWhiteSpace(value as string))
                    continue;

                updates.Add(updateBuilder.Set(prop.Name, value));
            }

            // Always update modified date
            updates.Add(updateBuilder.Set(x => x.CustomerDetailAddressUpdateDate, DateTime.UtcNow));

            if (updates.Count > 0)
            {
                var updateDefinition = updateBuilder.Combine(updates);
                await _collection.UpdateOneAsync(filter, updateDefinition);
            }
        }

        public async Task<CustomerDetailAddress> GetByIdAsync(string id)
        {
            var filter = Builders<CustomerDetailAddress>.Filter.Eq("_id", new ObjectId(id)) &
             Builders<CustomerDetailAddress>.Filter.Eq(x => x.CustomerDetailAddressStatus, "Y");
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<List<CustomerDetailAddress>> GetByCustomerIdAsync(string customerId)
        {
            var filter = Builders<CustomerDetailAddress>.Filter.Eq(c => c.CustomerId, customerId) &
             Builders<CustomerDetailAddress>.Filter.Eq(x => x.CustomerDetailAddressStatus, "Y");
            return await _collection.Find(filter).ToListAsync();
        }
       
       
        public async Task<CustomerDetailAddress> GetByCustomerIdAndAddressIdAsync(string customerId, string customerDetailAddressId)
        {
            var filter = Builders<CustomerDetailAddress>.Filter.And(
                Builders<CustomerDetailAddress>.Filter.Eq(x => x.CustomerId, customerId),
                Builders<CustomerDetailAddress>.Filter.Eq(x => x.CustomerDetailAddressId, customerDetailAddressId)
            );

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<List<CustomerDetailAddress>> GetAllAsync()
        {
            var filter = Builders<CustomerDetailAddress>.Filter.Eq(x => x.CustomerDetailAddressStatus, "Y");
            return await _collection.Find(filter).ToListAsync();
        }
        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<CustomerDetailAddress>.Filter.Eq("_id", new ObjectId(id));
            var update = Builders<CustomerDetailAddress>.Update
                .Set(x => x.CustomerDetailAddressStatus, "N")
                .Set(x => x.CustomerDetailAddressUpdateDate, DateTime.UtcNow);

            var result = await _collection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }

    }
}

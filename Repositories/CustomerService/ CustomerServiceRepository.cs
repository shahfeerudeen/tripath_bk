using MongoDB.Bson;
using MongoDB.Driver;
using tripath.Models;

namespace tripath.Repositories
{
    public class CustomerServiceRepository : ICustomerServiceRepository
    {
        private readonly IMongoCollection<CustomerServices> _collection;

        public CustomerServiceRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<CustomerServices>("CustomerServices");
        }

        public async Task<CustomerServices> CreateAsync(CustomerServices customer)
        {
            await _collection.InsertOneAsync(customer);
            return customer;
        }

        public async Task UpdateAsync(string id, CustomerServices updatedService)
        {
            var filter = Builders<CustomerServices>.Filter.Eq(x => x.CustomerServiceId, id);
            var updateBuilder = Builders<CustomerServices>.Update;
            var updates = new List<UpdateDefinition<CustomerServices>>();

            var properties = typeof(CustomerServices).GetProperties();
            foreach (var prop in properties)
{
    var value = prop.GetValue(updatedService);

    // Skip audit/system fields
    if (prop.Name == "CustomerId" ||
        prop.Name == "CustomerServiceId" ||
        prop.Name == "CustomerServiceEntryDate" ||
        prop.Name == "CustomerServiceCreatedBy")
        continue;

    // For nullable booleans and non-nullables, allow false too
    if (value == null && prop.PropertyType != typeof(bool) && prop.PropertyType != typeof(bool?))
        continue;

    // For strings, skip empty
    if (prop.PropertyType == typeof(string) && string.IsNullOrWhiteSpace(value as string))
        continue;

    updates.Add(updateBuilder.Set(prop.Name, value));
}

            updates.Add(updateBuilder.Set(x => x.CustomerServiceUpdateDate, DateTime.UtcNow));

            if (updates.Count > 0)
            {
                var updateDef = updateBuilder.Combine(updates);
                await _collection.UpdateOneAsync(filter, updateDef);
            }
        }

        public async Task<CustomerServices> GetByIdAsync(string id)
        {
            var filter = Builders<CustomerServices>.Filter.Eq(x => x.CustomerServiceId, id) &
                         Builders<CustomerServices>.Filter.Eq(x => x.CustomerServiceStatus, "Y");
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<CustomerServices> GetByCustomerIdAndServiceIdAsync(string customerId, string serviceId)
        {
            var filter = Builders<CustomerServices>.Filter.And(
                Builders<CustomerServices>.Filter.Eq(x => x.CustomerId, customerId),
                Builders<CustomerServices>.Filter.Eq(x => x.CustomerServiceId, serviceId),
                Builders<CustomerServices>.Filter.Eq(x => x.CustomerServiceStatus, "Y")
            );

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<CustomerServices> GetByCustomerIdAsync(string customerId)
        {
            var filter = Builders<CustomerServices>.Filter.Eq(x => x.CustomerId, customerId) &
                         Builders<CustomerServices>.Filter.Eq(x => x.CustomerServiceStatus, "Y");
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CustomerServices>> GetAllAsync()
        {
            var filter = Builders<CustomerServices>.Filter.Eq(x => x.CustomerServiceStatus, "Y");
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<bool> DeletedAsync(string id)
        {
            var filter = Builders<CustomerServices>.Filter.Eq(x => x.CustomerServiceId, id);
            var update = Builders<CustomerServices>.Update
                            .Set(x => x.CustomerServiceStatus, "N")
                            .Set(x => x.CustomerServiceUpdateDate, DateTime.UtcNow);

            var result = await _collection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }
    }
}

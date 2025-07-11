using MongoDB.Bson;
using MongoDB.Driver;
using tripath.Models;

namespace tripath.Repositories
{
    public class CustomerRegRepository : ICustomerRegRepository
    {
        private readonly IMongoCollection<CustomerReg> _collection;

        public CustomerRegRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<CustomerReg>("CustomerReg");
        }

        public async Task<CustomerReg> CreateAsync(CustomerReg reg)
        {
            reg.CustomerRegistrationStatus = "Y";
            reg.CustomerRegistrationEntryDate = DateTime.UtcNow;
            reg.CustomerRegistrationUpdateDate = DateTime.UtcNow;
            await _collection.InsertOneAsync(reg);
            return reg;
        }

        public async Task UpdateAsync(string id, CustomerReg updated)
        {
            var filter = Builders<CustomerReg>.Filter.Eq("_id", new ObjectId(id));
            var updateBuilder = Builders<CustomerReg>.Update;
            var updates = new List<UpdateDefinition<CustomerReg>>();

            var properties = typeof(CustomerReg).GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(updated);

                // Skip nulls, default values, and system fields
                if (value == null ||
                    prop.Name == "CustomerId" ||
                    prop.Name == "CustomerRegistrationId" ||
                    prop.Name == "CustomerRegCreateddBy" ||
                    prop.Name == "CustomerRegistrationEntryDate")
                    continue;

                // For strings, skip empty
                if (prop.PropertyType == typeof(string) && string.IsNullOrWhiteSpace(value as string))
                    continue;

                // For DateTime, skip default
                if (prop.PropertyType == typeof(DateTime) && ((DateTime)value) == default)
                    continue;

                updates.Add(updateBuilder.Set(prop.Name, value));
            }

            // Always update modified date
            updates.Add(updateBuilder.Set(x => x.CustomerRegistrationUpdateDate, DateTime.UtcNow));

            if (updates.Count > 0)
            {
                var updateDefinition = updateBuilder.Combine(updates);
                await _collection.UpdateOneAsync(filter, updateDefinition);
            }
        }

        public async Task<CustomerReg> UpsertAsync(CustomerReg reg)
        {
            if (string.IsNullOrEmpty(reg.CustomerRegistrationId))
                return await CreateAsync(reg);

            await UpdateAsync(reg.CustomerRegistrationId, reg);
            return await GetByIdAsync(reg.CustomerRegistrationId);
        }

        public async Task<CustomerReg> GetByIdAsync(string id)
        {
            var filter = Builders<CustomerReg>.Filter.Eq("_id", new ObjectId(id)) &
                         Builders<CustomerReg>.Filter.Eq(x => x.CustomerRegistrationStatus, "Y");

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<CustomerReg>> GetByCustomerIdAsync(string customerId)
        {
            var filter = Builders<CustomerReg>.Filter.Eq(x => x.CustomerId, customerId) &
                         Builders<CustomerReg>.Filter.Eq(x => x.CustomerRegistrationStatus, "Y");

            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<List<CustomerReg>> GetAllAsync()
        {
            var filter = Builders<CustomerReg>.Filter.Eq(x => x.CustomerRegistrationStatus, "Y");
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<CustomerReg?> GetByCustomerIdAndRegIdAsync(string customerId, string customerRegistrationId)
        {
            var filter = Builders<CustomerReg>.Filter.And(
                Builders<CustomerReg>.Filter.Eq(x => x.CustomerId, customerId),
                Builders<CustomerReg>.Filter.Eq(x => x.CustomerRegistrationId, customerRegistrationId),
                Builders<CustomerReg>.Filter.Eq(x => x.CustomerRegistrationStatus, "Y")
            );

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<CustomerReg>.Filter.Eq("_id", new ObjectId(id));
            var update = Builders<CustomerReg>.Update
                .Set(x => x.CustomerRegistrationStatus, "N")
                .Set(x => x.CustomerRegistrationUpdateDate, DateTime.UtcNow);

            var result = await _collection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }
    }
}

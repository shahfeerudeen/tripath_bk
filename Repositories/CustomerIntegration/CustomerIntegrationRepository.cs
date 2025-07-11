using MongoDB.Bson;
using MongoDB.Driver;
using tripath.Models;

namespace tripath.Repositories
{
    public class CustomerIntegrationRepository : ICustomerIntegrationRepository
    {
        private readonly IMongoCollection<CustomerIntegration> _collection;

        public CustomerIntegrationRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<CustomerIntegration>("CustomerIntegration");
        }

        public async Task<CustomerIntegration> CreateAsync(CustomerIntegration integration)
        {
            await _collection.InsertOneAsync(integration);
            return integration;
        }

        public async Task UpdateAsync(string id, CustomerIntegration updatedIntegration)
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return;

            var filter = Builders<CustomerIntegration>.Filter.Eq("_id", objectId);
            var updateBuilder = Builders<CustomerIntegration>.Update;
            var updates = new List<UpdateDefinition<CustomerIntegration>>();

            var properties = typeof(CustomerIntegration).GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(updatedIntegration);

                // Skip null or system fields
                if (value == null ||
                    prop.Name == "CustomerIntegrationId" ||
                    prop.Name == "CustomerIntegrationEntryDate" ||
                    prop.Name == "CustomerIntegrationCreatedBy")
                    continue;

                if (prop.PropertyType == typeof(string) && string.IsNullOrWhiteSpace(value as string))
                    continue;

                updates.Add(updateBuilder.Set(prop.Name, value));
            }

            // Always set update date
            updates.Add(updateBuilder.Set(x => x.CustomIntegrationUpdateDate, DateTime.UtcNow));

            if (updates.Count > 0)
            {
                var updateDefinition = updateBuilder.Combine(updates);
                await _collection.UpdateOneAsync(filter, updateDefinition);
            }
        }

        public async Task<CustomerIntegration?> GetByIdAsync(string id)
        {
            var filter = Builders<CustomerIntegration>.Filter.Eq("_id", new ObjectId(id)) &
                         Builders<CustomerIntegration>.Filter.Eq(x => x.CustomerIntegrationStatus, "Y");

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<CustomerIntegration>> GetByCustomerIdAsync(string customerId)
        {
            var filter = Builders<CustomerIntegration>.Filter.Eq(x => x.CustomerId, customerId) &
                         Builders<CustomerIntegration>.Filter.Eq(x => x.CustomerIntegrationStatus, "Y");

            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<List<CustomerIntegration>> GetAllAsync()
        {
            var filter = Builders<CustomerIntegration>.Filter.Eq(x => x.CustomerIntegrationStatus, "Y");
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<CustomerIntegration>.Filter.Eq("_id", new ObjectId(id));
            var update = Builders<CustomerIntegration>.Update
                .Set(x => x.CustomerIntegrationStatus, "N")
                .Set(x => x.CustomIntegrationUpdateDate, DateTime.UtcNow);

            var result = await _collection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }
    }
}

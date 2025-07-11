using MongoDB.Bson;
using MongoDB.Driver;
using tripath.Models;

namespace tripath.Repositories
{
    public class CustomerContactRepository : ICustomerContactRepository
    {
        private readonly IMongoCollection<CustomerContact> _collection;

        public CustomerContactRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<CustomerContact>("CustomerContact");
        }

        public async Task<CustomerContact> CreateAsync(CustomerContact contact)
        {
            await _collection.InsertOneAsync(contact);
            return contact;
        }

        public async Task UpdateAsync(string id, CustomerContact updatedContact)
        {
            var filter = Builders<CustomerContact>.Filter.Eq("_id", new ObjectId(id));
            var updateBuilder = Builders<CustomerContact>.Update;
            var updates = new List<UpdateDefinition<CustomerContact>>();

            var properties = typeof(CustomerContact).GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(updatedContact);

                // Skip nulls and system fields
                if (value == null ||
                    prop.Name == nameof(CustomerContact.CustomerContactId) ||
                    prop.Name == nameof(CustomerContact.CustomerContactEntryDate) ||
                    prop.Name == nameof(CustomerContact.CustomerContactCreatedBy))
                    continue;

                // For strings, skip if empty
                if (prop.PropertyType == typeof(string) && string.IsNullOrWhiteSpace(value as string))
                    continue;

                updates.Add(updateBuilder.Set(prop.Name, value));
            }

            // Always update modified date
            updates.Add(updateBuilder.Set(x => x.CustomerContactUpdateDate, DateTime.UtcNow));

            if (updates.Count > 0)
            {
                var updateDefinition = updateBuilder.Combine(updates);
                await _collection.UpdateOneAsync(filter, updateDefinition);
            }
        }

        public async Task<List<CustomerContact>> GetAllAsync()
        {
            var filter = Builders<CustomerContact>.Filter.Eq(x => x.CustomerContactStatus, "Y");
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<List<CustomerContact>> GetByCustomerIdAsync(string customerId)
        {
            var filter = Builders<CustomerContact>.Filter.Eq(x => x.CustomerId, customerId) &
                         Builders<CustomerContact>.Filter.Eq(x => x.CustomerContactStatus, "Y");
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<CustomerContact> GetByIdAsync(string contactId)
        {
            var filter = Builders<CustomerContact>.Filter.Eq(x => x.CustomerContactId, contactId) &
                         Builders<CustomerContact>.Filter.Eq(x => x.CustomerContactStatus, "Y");
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAsync(string contactId)
        {
            var filter = Builders<CustomerContact>.Filter.Eq(x => x.CustomerContactId, contactId);
            var update = Builders<CustomerContact>.Update
                .Set(x => x.CustomerContactStatus, "N")
                .Set(x => x.CustomerContactUpdateDate, DateTime.UtcNow);

            var result = await _collection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }
    }
}

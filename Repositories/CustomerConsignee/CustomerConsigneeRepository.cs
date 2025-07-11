using MongoDB.Bson;
using MongoDB.Driver;
using tripath.Models;

namespace tripath.Repositories
{
    public class CustomerConsigneeRepository : ICustomerConsigneeRepository
    {
        private readonly IMongoCollection<CustomerConsignee> _collection;

        public CustomerConsigneeRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<CustomerConsignee>("CustomerConsignee");
        }

        public async Task<CustomerConsignee> CreateAsync(CustomerConsignee consignee)
        {
            await _collection.InsertOneAsync(consignee);
            return consignee;
        }

        public async Task UpdateAsync(string id, CustomerConsignee updatedConsignee)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                Console.WriteLine($"Invalid ObjectId format for ID: {id}");
                return;
            }

            var filter = Builders<CustomerConsignee>.Filter.Eq("_id", objectId);
            var updateBuilder = Builders<CustomerConsignee>.Update;
            var updates = new List<UpdateDefinition<CustomerConsignee>>();

            var properties = typeof(CustomerConsignee).GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(updatedConsignee);

                // Skip nulls and system-controlled fields
                if (value == null ||
                    prop.Name == "CustomerConsigneeId" ||
                    prop.Name == "CustomerConsigneeEntryDate" ||
                    prop.Name == "CustomerConsigneeCreatedBy")
                    continue;

                // Skip empty strings
                if (prop.PropertyType == typeof(string) && string.IsNullOrWhiteSpace(value as string))
                    continue;

                updates.Add(updateBuilder.Set(prop.Name, value));
            }

            // Always update modified timestamp
            updates.Add(updateBuilder.Set(x => x.CustomerConsigneeUpdateDate, DateTime.UtcNow));

            if (updates.Count > 0)
            {
                var updateDefinition = updateBuilder.Combine(updates);
                var result = await _collection.UpdateOneAsync(filter, updateDefinition);

                Console.WriteLine($"Matched: {result.MatchedCount}, Modified: {result.ModifiedCount}");
            }
        }


        public async Task<List<CustomerConsignee>> GetAllAsync()
        {
            var filter = Builders<CustomerConsignee>.Filter.Eq(x => x.CustomerConsigneeStatus, "Y");
            return await _collection.Find(filter).ToListAsync();
        }
public async Task<CustomerConsignee?> GetByCustomerIdAndConsigneeIdAsync(string customerId, string consigneeId)
{
    var filter = Builders<CustomerConsignee>.Filter.And(
        Builders<CustomerConsignee>.Filter.Eq(x => x.CustomerId, customerId),
        Builders<CustomerConsignee>.Filter.Eq(x => x.CustomerConsigneeId, consigneeId),
        Builders<CustomerConsignee>.Filter.Eq(x => x.CustomerConsigneeStatus, "Y")
    );

    return await _collection.Find(filter).FirstOrDefaultAsync();
}

        public async Task<CustomerConsignee> GetByIdAsync(string consigneeId)
        {
            var filter = Builders<CustomerConsignee>.Filter.Eq(x => x.CustomerConsigneeId, consigneeId) &
                         Builders<CustomerConsignee>.Filter.Eq(x => x.CustomerConsigneeStatus, "Y");
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<CustomerConsignee>> GetByCustomerIdAsync(string customerId)
        {
            var filter = Builders<CustomerConsignee>.Filter.Eq(x => x.CustomerId, customerId) &
                         Builders<CustomerConsignee>.Filter.Eq(x => x.CustomerConsigneeStatus, "Y");
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<bool> DeleteAsync(string consigneeId)
        {
            var filter = Builders<CustomerConsignee>.Filter.Eq(x => x.CustomerConsigneeId, consigneeId);
            var update = Builders<CustomerConsignee>.Update
                .Set(x => x.CustomerConsigneeStatus, "N")
                .Set(x => x.CustomerConsigneeUpdateDate, DateTime.UtcNow);

            var result = await _collection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }
    }
}

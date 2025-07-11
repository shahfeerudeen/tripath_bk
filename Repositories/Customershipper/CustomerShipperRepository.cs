using MongoDB.Bson;
using MongoDB.Driver;
using tripath.Models;

namespace tripath.Repositories
{
    public class CustomerShipperRepository : ICustomerShipperRepository
    {
        private readonly IMongoCollection<CustomerShipper> _collection;

        public CustomerShipperRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<CustomerShipper>("CustomerShipper");
        }

        public async Task<CustomerShipper> CreateAsync(CustomerShipper shipper)
        {
            await _collection.InsertOneAsync(shipper);
            return shipper;
        }

        public async Task UpdateAsync(string id, CustomerShipper updatedShipper)
        {
            if (!ObjectId.TryParse(id, out var objectId))
                return;

            var filter = Builders<CustomerShipper>.Filter.Eq("_id", objectId);
            var updateBuilder = Builders<CustomerShipper>.Update;
            var updates = new List<UpdateDefinition<CustomerShipper>>();

            var properties = typeof(CustomerShipper).GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(updatedShipper);

                // Skip null or system-controlled fields
                if (value == null ||
                    prop.Name == "CustomerShipperId" ||
                    prop.Name == "CustomerShippeCreatedBy" ||
                    prop.Name == "CustomerShipperEntryDate")
                    continue;

                if (prop.PropertyType == typeof(string) && string.IsNullOrWhiteSpace(value as string))
                    continue;

                updates.Add(updateBuilder.Set(prop.Name, value));
            }

            // Always set update timestamp
            updates.Add(updateBuilder.Set(x => x.CustomerShipperUpdateDate, DateTime.UtcNow));

            if (updates.Count > 0)
            {
                var updateDefinition = updateBuilder.Combine(updates);
                await _collection.UpdateOneAsync(filter, updateDefinition);
            }
        }

        public async Task<List<CustomerShipper>> GetByCustomerIdAsync(string customerId)
        {
            var filter = Builders<CustomerShipper>.Filter.Eq(x => x.CustomerId, customerId) &
                         Builders<CustomerShipper>.Filter.Eq(x => x.CustomerShipperStatus, "Y");
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<List<CustomerShipper>> GetAllAsync()
        {
            var filter = Builders<CustomerShipper>.Filter.Eq(x => x.CustomerShipperStatus, "Y");
            return await _collection.Find(filter).ToListAsync();
        }

       public async Task<CustomerShipper?> GetByIdAsync(string id)

        {
            var filter = Builders<CustomerShipper>.Filter.Eq(x => x.CustomerShipperId, id) &
                         Builders<CustomerShipper>.Filter.Eq(x => x.CustomerShipperStatus, "Y");
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<CustomerShipper?> GetByCustomerIdAndShipperIdAsync(string customerId, string shipperId)

        {
            var filter = Builders<CustomerShipper>.Filter.And(
                Builders<CustomerShipper>.Filter.Eq(x => x.CustomerId, customerId),
                Builders<CustomerShipper>.Filter.Eq(x => x.CustomerShipperId, shipperId),
                Builders<CustomerShipper>.Filter.Eq(x => x.CustomerShipperStatus, "Y")
            );

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<CustomerShipper>.Filter.Eq(x => x.CustomerShipperId, id);
            var update = Builders<CustomerShipper>.Update
                .Set(x => x.CustomerShipperStatus, "N")
                .Set(x => x.CustomerShipperUpdateDate, DateTime.UtcNow);

            var result = await _collection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }
    }
}

using MongoDB.Bson;
using MongoDB.Driver;
using tripath.Models;

public class CustomerCarrierRepository : ICustomerCarrierRepository
{
    private readonly IMongoCollection<CustomerCarrier> _collection;

    public CustomerCarrierRepository(IMongoDatabase db)
    {
        _collection = db.GetCollection<CustomerCarrier>("CustomerCarrier");
    }

    public async Task<CustomerCarrier> CreateAsync(CustomerCarrier carrier)
    {
        await _collection.InsertOneAsync(carrier);
        return carrier;
    }

    public async Task UpdateAsync(string id, CustomerCarrier carrier)
    {
        var filter = Builders<CustomerCarrier>.Filter.Eq("_id", new ObjectId(id));
        var updateBuilder = Builders<CustomerCarrier>.Update;
        var updates = new List<UpdateDefinition<CustomerCarrier>>();

        var properties = typeof(CustomerCarrier).GetProperties();
        foreach (var prop in properties)
        {
            var value = prop.GetValue(carrier);

            // Skip nulls and system fields
            if (value == null ||
                prop.Name == "CustomerId" ||
                prop.Name == "CustomerCarrierId" ||
                prop.Name == "CustomerCarrierEntryDate" ||
                prop.Name == "CustomerCarrierCreatedBy")
                continue;

            if (prop.PropertyType == typeof(string) && string.IsNullOrWhiteSpace(value as string))
                continue;

            updates.Add(updateBuilder.Set(prop.Name, value));
        }

        // Always update modified date
        updates.Add(updateBuilder.Set(x => x.CustomerCarrierUpdateDate, DateTime.UtcNow));

        if (updates.Count > 0)
        {
            var updateDefinition = updateBuilder.Combine(updates);
            await _collection.UpdateOneAsync(filter, updateDefinition);
        }
    }

    public async Task<CustomerCarrier> GetByIdAsync(string id)
    {
        var filter = Builders<CustomerCarrier>.Filter.Eq(x => x.CustomerCarrierId, id) &
                     Builders<CustomerCarrier>.Filter.Eq(x => x.CustomerCarrierStatus, "Y");
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<CustomerCarrier>> GetByCustomerIdAsync(string customerId)
    {
        var filter = Builders<CustomerCarrier>.Filter.Eq(x => x.CustomerId, customerId) &
                     Builders<CustomerCarrier>.Filter.Eq(x => x.CustomerCarrierStatus, "Y");
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<List<CustomerCarrier>> GetAllAsync()
    {
        var filter = Builders<CustomerCarrier>.Filter.Eq(x => x.CustomerCarrierStatus, "Y");
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<bool> SoftDeleteAsync(string id)
    {
        var filter = Builders<CustomerCarrier>.Filter.Eq("_id", new ObjectId(id));
        var update = Builders<CustomerCarrier>.Update
            .Set(x => x.CustomerCarrierStatus, "N")
            .Set(x => x.CustomerCarrierUpdateDate, DateTime.UtcNow);

        var result = await _collection.UpdateOneAsync(filter, update);
        return result.ModifiedCount > 0;
    }
}

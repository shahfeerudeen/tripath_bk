using Data;
using MongoDB.Driver;
using tripath.Models;

namespace tripath.Repositories;

public class CustomerMasterRepository : ICustomerMasterRepository
{
    private readonly IMongoCollection<CustomerMaster> _collection;
    private readonly IMongoCollection<CustomerAddress> _addressCollection;

    public CustomerMasterRepository(DbContextClass context)
    {
        _collection = context.Database.GetCollection<CustomerMaster>("CustomerMaster");
        _addressCollection = context.Database.GetCollection<CustomerAddress>("CustomerAddress");
    }

    // Insert CustomerMaster + CustomerAddress
    public async Task<CustomerMaster> CreateAsync(CustomerMaster master, CustomerAddress address)
    {
        // Insert CustomerMaster
        await _collection.InsertOneAsync(master);

        // Link CustomerId to address and insert address
        address.CustomerId = master.CustomerId!;
        await _addressCollection.InsertOneAsync(address);

        return master;
    }

    // Existing methods remain unchanged
    public async Task<CustomerMaster> CreateAsync(CustomerMaster customer)
    {
        await _collection.InsertOneAsync(customer);
        return customer;
    }

    public async Task<CustomerMaster?> PartialUpdateAsync(
        string customerId,
        CustomerMasterUpdateModel updated,
        string updatedBy
    )
    {
        var filter = Builders<CustomerMaster>.Filter.Eq(x => x.CustomerId, customerId);

        var updateDef = new List<UpdateDefinition<CustomerMaster>>();
        var builder = Builders<CustomerMaster>.Update;

        if (updated.CustomerName != null)
            updateDef.Add(builder.Set(x => x.CustomerName, updated.CustomerName));
        if (updated.AliseName != null)
            updateDef.Add(builder.Set(x => x.AliseName, updated.AliseName));
        if (updated.CountryId != null)
            updateDef.Add(builder.Set(x => x.CountryId, updated.CountryId));
        if (updated.StateId != null)
            updateDef.Add(builder.Set(x => x.StateId, updated.StateId));
        if (updated.CityId != null)
            updateDef.Add(builder.Set(x => x.CityId, updated.CityId));

        updateDef.Add(builder.Set(x => x.CustomerUpdateDate, DateTime.UtcNow));

        var finalUpdate = builder.Combine(updateDef);
        var result = await _collection.UpdateOneAsync(filter, finalUpdate);

        return result.ModifiedCount > 0
            ? await _collection.Find(filter).FirstOrDefaultAsync()
            : null;
    }

    public async Task<CustomerWithAddressResponse?> GetByIdWithAddressAsync(string customerId)
    {
        var filter = Builders<CustomerMaster>.Filter.And(
            Builders<CustomerMaster>.Filter.Eq(x => x.CustomerId, customerId),
            Builders<CustomerMaster>.Filter.Eq(x => x.CustomerStatus, "Y")
        );

        var customer = await _collection.Find(filter).FirstOrDefaultAsync();
        if (customer == null)
            return null;

        var address = await _addressCollection
            .Find(x => x.CustomerId == customer.CustomerId)
            .FirstOrDefaultAsync();

        return new CustomerWithAddressResponse
        {
            Master = customer,
            Addresses =
                address != null
                    ? new List<CustomerAddress> { address }
                    : new List<CustomerAddress>(), // ✅ single address
        };
    }

    public async Task<IEnumerable<CustomerWithAddressResponse>> GetAllAsync()
    {
        var masterFilter = Builders<CustomerMaster>.Filter.Eq(x => x.CustomerStatus, "Y");
        var masters = await _collection.Find(masterFilter).ToListAsync();

        var result = new List<CustomerWithAddressResponse>();

        foreach (var master in masters)
        {
            var addressFilter = Builders<CustomerAddress>.Filter.Eq(
                x => x.CustomerId,
                master.CustomerId
            );
            var addresses = await _addressCollection.Find(addressFilter).ToListAsync();

            result.Add(new CustomerWithAddressResponse { Master = master, Addresses = addresses });
        }

        return result;
    }

    public async Task<bool> DeleteAsync(string customerId)
    {
        var filter = Builders<CustomerMaster>.Filter.Eq(x => x.CustomerId, customerId);
        var update = Builders<CustomerMaster>
            .Update.Set(x => x.CustomerStatus, "N")
            .Set(x => x.CustomerUpdateDate, DateTime.UtcNow);

        var result = await _collection.UpdateOneAsync(filter, update);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> ExistsAsync(string customerId)
    {
        var filter = Builders<CustomerMaster>.Filter.And(
            Builders<CustomerMaster>.Filter.Eq(x => x.CustomerId, customerId),
            Builders<CustomerMaster>.Filter.Eq(x => x.CustomerStatus, "Y")
        );

        return await _collection.Find(filter).AnyAsync();
    }
}

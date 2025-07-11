using MediatR;
using MongoDB.Driver;
using tripath.Commands;
using tripath.Models;

public class CheckCustomerEmailExistsHandler : IRequestHandler<CheckCustomerEmailExistsQuery, bool>
{
    private readonly IMongoDatabase _mongoDatabase;

    public CheckCustomerEmailExistsHandler(IMongoDatabase mongoDatabase)
    {
        _mongoDatabase = mongoDatabase;
    }

    public async Task<bool> Handle(
        CheckCustomerEmailExistsQuery request,
        CancellationToken cancellationToken
    )
    {
        var collection = _mongoDatabase.GetCollection<CustomerAddress>("CustomerAddress");
        var exists = await collection
            .Find(x => x.CustomerAddressEmailAddress == request.Email)
            .AnyAsync(cancellationToken);

        return exists;
    }
}

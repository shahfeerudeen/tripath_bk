using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using tripath.Models;
using tripath.Queries;

namespace tripath.Handlers
{
    public class FilterCustomerWithAddressHandler
        : IRequestHandler<FilterCustomerWithAddressQuery, List<CreateCustomerWithAddressRequest>>
    {
        private readonly IMongoCollection<CustomerMaster> _customerCollection;

        public FilterCustomerWithAddressHandler(Data.DbContextClass context)
        {
            _customerCollection = context.Database.GetCollection<CustomerMaster>("CustomerMaster");
        }

        public async Task<List<CreateCustomerWithAddressRequest>> Handle(
            FilterCustomerWithAddressQuery request,
            CancellationToken cancellationToken
        )
        {
            var builder = Builders<CustomerMaster>.Filter;
            var filters = new List<FilterDefinition<CustomerMaster>>();

            filters.AddRange(
                new[]
                {
                    !string.IsNullOrEmpty(request.Name)
                        ? builder.Regex(
                            "CustomerName",
                            new BsonRegularExpression(request.Name, "i")
                        )
                        : null,
                    !string.IsNullOrEmpty(request.Alias)
                        ? builder.Regex("AliseName", new BsonRegularExpression(request.Alias, "i"))
                        : null,
                    request.Active.HasValue
                        ? builder.Eq("CustomerStatus", request.Active.Value ? "Y" : "N")
                        : null,
                    !string.IsNullOrEmpty(request.CountryId)
                        ? builder.Eq("CountryId", request.CountryId)
                        : null,
                }.Where(f => f != null)
            );
            var combinedFilter = filters.Count > 0 ? builder.And(filters) : builder.Empty;

            var renderedFilter = combinedFilter.Render(
                new RenderArgs<CustomerMaster>(
                    BsonSerializer.SerializerRegistry.GetSerializer<CustomerMaster>(),
                    BsonSerializer.SerializerRegistry
                )
            );

            var matchStage = new BsonDocument("$match", renderedFilter);

            var lookupStage = new BsonDocument(
                "$lookup",
                new BsonDocument
                {
                    { "from", "CustomerAddress" },
                    { "localField", "_id" },
                    { "foreignField", "CustomerId" },
                    { "as", "Addresses" },
                }
            );

            var pipeline = new[] { matchStage, lookupStage };
            var documents = await _customerCollection
                .Aggregate<BsonDocument>(pipeline)
                .ToListAsync(cancellationToken);

            var result = new List<CreateCustomerWithAddressRequest>();

            foreach (var doc in documents)
            {
                var master = BsonSerializer.Deserialize<CustomerMaster>(doc);
                var addresses = doc["Addresses"]
                    .AsBsonArray.Select(a =>
                        BsonSerializer.Deserialize<CustomerAddress>(a.AsBsonDocument)
                    )
                    .ToList();

                addresses = addresses
                    .Where(a =>
                        (
                            string.IsNullOrEmpty(request.BranchName)
                            || a.CustomerBranchName.Contains(
                                request.BranchName,
                                StringComparison.OrdinalIgnoreCase
                            )
                        )
                        && (
                            string.IsNullOrEmpty(request.AddressLine)
                            || (
                                a.CustomerAddressLine?.Contains(
                                    request.AddressLine,
                                    StringComparison.OrdinalIgnoreCase
                                ) ?? false
                            )
                        )
                        && (
                            string.IsNullOrEmpty(request.Telephone)
                            || a.CustomerAddressTelephone == request.Telephone
                        )
                        && (
                            string.IsNullOrEmpty(request.Website)
                            || (
                                a.CustomerAddressWebsite?.Contains(
                                    request.Website,
                                    StringComparison.OrdinalIgnoreCase
                                ) ?? false
                            )
                        )
                        && (
                            string.IsNullOrEmpty(request.EmailAddress)
                            || a.CustomerAddressEmailAddress == request.EmailAddress
                        )
                        && (
                            string.IsNullOrEmpty(request.SalesPerson)
                            || (
                                a.CustomerAddressSalesPerson?.Contains(
                                    request.SalesPerson,
                                    StringComparison.OrdinalIgnoreCase
                                ) ?? false
                            )
                        )
                        && (
                            string.IsNullOrEmpty(request.CollectionExec)
                            || (
                                a.CustomerAddressCollectionExec?.Contains(
                                    request.CollectionExec,
                                    StringComparison.OrdinalIgnoreCase
                                ) ?? false
                            )
                        )
                        && (
                            string.IsNullOrEmpty(request.TaxableType)
                            || a.CustomerAddressTaxableType == request.TaxableType
                        )
                        && (
                            string.IsNullOrEmpty(request.Fax) || a.CustomerAddressFax == request.Fax
                        )
                        && (
                            string.IsNullOrEmpty(request.PostalCode)
                            || a.CustomerAddressPostalCode == request.PostalCode
                        )
                        && (
                            string.IsNullOrEmpty(request.LOBWise)
                            || a.CustomerAddressLOBWise == request.LOBWise
                        )
                        && (
                            string.IsNullOrEmpty(request.AddressStatus)
                            || a.CustomerAddressStatus == request.AddressStatus
                        )
                        && (
                            !request.IsSetAsDefault.HasValue
                            || a.CustomerAddressIsSetAsDefault == request.IsSetAsDefault.Value
                        )
                        && (
                            !request.IsDeactivate.HasValue
                            || a.CustomerAddressIsDeactivate == request.IsDeactivate.Value
                        )
                    )
                    .ToList();

                if (addresses.Any())
                {
                    result.Add(
                        new CreateCustomerWithAddressRequest
                        {
                            Master = master,
                            Address = addresses.FirstOrDefault(),
                        }
                    );
                }
            }

            return result;
        }
    }
}

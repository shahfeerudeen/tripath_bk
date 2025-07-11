using MediatR;
using tripath.Models;

namespace tripath.Queries
{
    public class FilterCustomerWithAddressQuery : IRequest<List<CreateCustomerWithAddressRequest>>
    {
        // CustomerMaster filters
        public string? Name { get; set; }
        public string? Alias { get; set; }
        public bool? Active { get; set; }
        public List<string>? UserTypesId { get; set; }
        public string? ApprovalStatus { get; set; }
        public string? CountryId { get; set; }

        // CustomerAddress filters
        public string? BranchName { get; set; }
        public string? AddressLine { get; set; }
        public string? Telephone { get; set; }
        public string? Website { get; set; }
        public string? EmailAddress { get; set; }
        public string? SalesPerson { get; set; }
        public string? CollectionExec { get; set; }
        public string? TaxableType { get; set; }
        public string? Fax { get; set; }
        public string? PostalCode { get; set; }
        public string? LOBWise { get; set; }
        public string? AddressStatus { get; set; }
        public bool? IsSetAsDefault { get; set; }
        public bool? IsDeactivate { get; set; }
    }
}

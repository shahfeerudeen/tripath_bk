using MediatR;
using tripath.Commands;
using tripath.Exceptions;
using tripath.Models;
using tripath.Repositories;
using tripath.Utils;

namespace tripath.Handlers
{
    public class UpsertCustomerShipperHandler : IRequestHandler<UpsertCustomerShipperCommand, CustomerShipper>
    {
        private readonly ICustomerShipperRepository _repository;
        private readonly ICustomerMasterRepository _customerRepo;

        public UpsertCustomerShipperHandler(
            ICustomerShipperRepository repository,
            ICustomerMasterRepository customerRepo)
        {
            _repository = repository;
            _customerRepo = customerRepo;
        }

        public async Task<CustomerShipper> Handle(UpsertCustomerShipperCommand request, CancellationToken cancellationToken)
        {
            var shipper = request.Customer;

            // 1. Optional: Add empty-field validation if needed
            if (CustomerValidationHelper.IsCustomerShipperEmpty(shipper))
                throw new ApiValidationException("At least one editable field is required.");

            // 2. Validate CustomerId
            var isCustomerValid = await _customerRepo.ExistsAsync(shipper.CustomerId!);
            if (!isCustomerValid)
                throw new ApiValidationException("Invalid CustomerId.");

            // 3. Create or Update
            if (string.IsNullOrEmpty(shipper.CustomerShipperId))
            {
                shipper.CustomerShipperEntryDate = DateTime.UtcNow;
                shipper.CustomerShippeCreatedBy = request.CustomerUpdatedBy;
                shipper.CustomerShipperStatus = "Y";

                await _repository.CreateAsync(shipper);
            }
            else
            {
                var existing = await _repository.GetByCustomerIdAndShipperIdAsync(shipper.CustomerId!, shipper.CustomerShipperId);
                if (existing == null)
                    throw new ApiValidationException("Invalid CustomerShipperId for given CustomerId.");

                shipper.CustomerUpdatedBy = request.CustomerUpdatedBy;
                await _repository.UpdateAsync(shipper.CustomerShipperId, shipper);
            }

            return shipper;
        }
    }
}

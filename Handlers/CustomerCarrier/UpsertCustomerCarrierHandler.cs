using MediatR;
using tripath.Commands;
using tripath.Exceptions;
using tripath.Models;
using tripath.Repositories;
using tripath.Utils;

namespace tripath.Handlers
{
    public class UpsertCustomerCarrierHandler : IRequestHandler<UpsertCustomerCarrierCommand, CustomerCarrier>
    {
        private readonly ICustomerCarrierRepository _repository;
        private readonly ICustomerMasterRepository _customerRepo;

        public UpsertCustomerCarrierHandler(
            ICustomerCarrierRepository repository,
            ICustomerMasterRepository customerRepo)
        {
            _repository = repository;
            _customerRepo = customerRepo;
        }

        public async Task<CustomerCarrier> Handle(UpsertCustomerCarrierCommand request, CancellationToken cancellationToken)
        {
            var carrier = request.Customer;

            // Validate at least one field is present
            if (CustomerValidationHelper.IsCustomerCarrierEmpty(carrier))
                throw new ApiValidationException("At least one editable field is required.");

            // Validate CustomerId
            var isCustomerValid = await _customerRepo.ExistsAsync(carrier.CustomerId!);
            if (!isCustomerValid)
                throw new ApiValidationException("Invalid CustomerId.");

            if (string.IsNullOrEmpty(carrier.CustomerCarrierId))
            {
                // Create new
                carrier.CustomerCarrierEntryDate = DateTime.UtcNow;
                carrier.CustomerCarrierCreatedBy = request.CustomerUpdatedBy;
                await _repository.CreateAsync(carrier);
            }
            else
            {
                // Update
                var existing = await _repository.GetByIdAsync(carrier.CustomerCarrierId);
                if (existing == null)
                    throw new ApiValidationException("Invalid CustomerCarrier Id for given CustomerId.");

                carrier.CustomerUpdatedBy = request.CustomerUpdatedBy;
                carrier.CustomerCarrierUpdateDate = DateTime.UtcNow;

                await _repository.UpdateAsync(carrier.CustomerCarrierId, carrier);
            }

            return carrier;
        }
    }
}

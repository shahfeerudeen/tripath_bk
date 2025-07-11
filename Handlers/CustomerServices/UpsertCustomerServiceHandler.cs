using MediatR;
using tripath.Commands;
using tripath.Exceptions;
using tripath.Models;
using tripath.Repositories;
using tripath.Utils;

namespace tripath.Handlers
{
    public class UpsertCustomerServiceHandler : IRequestHandler<UpsertCustomerServiceCommand, CustomerServices>
    {
        private readonly ICustomerServiceRepository _repository;
        private readonly ICustomerMasterRepository _customerRepo;

        public UpsertCustomerServiceHandler(
            ICustomerServiceRepository repository,
            ICustomerMasterRepository customerRepo)
        {
            _repository = repository;
            _customerRepo = customerRepo;
        }

        public async Task<CustomerServices> Handle(UpsertCustomerServiceCommand request, CancellationToken cancellationToken)
        {
            var service = request.Service;


            if (CustomerValidationHelper.IsCustomerServicesEmpty(service))
                throw new ApiValidationException("At least one editable field is required.");

            // Validate CustomerId
            var isValid = await _customerRepo.ExistsAsync(service.CustomerId!);
            if (!isValid)
                throw new ApiValidationException("Invalid CustomerId.");

            if (string.IsNullOrEmpty(service.CustomerServiceId))
            {
                // New entry
                service.CustomerServiceEntryDate = DateTime.UtcNow;
                service.CustomerServiceCreatedBy = request.CustomerUpdatedBy;

                await _repository.CreateAsync(service);
            }
            else
            {
                // Check if record exists
                var existing = await _repository.GetByCustomerIdAndServiceIdAsync(service.CustomerId!, service.CustomerServiceId);
                if (existing == null)
                    throw new ApiValidationException("Invalid CustomerService Id for given CustomerId.");

                service.CustomerUpdatedBy = request.CustomerUpdatedBy;
                await _repository.UpdateAsync(service.CustomerServiceId, service);
            }

            return service;
        }
    }
}

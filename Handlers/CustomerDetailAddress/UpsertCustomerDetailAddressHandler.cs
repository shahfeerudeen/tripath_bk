using MediatR;
using tripath.Commands;
using tripath.Exceptions;
using tripath.Models;
using tripath.Repositories;
using tripath.Utils;

namespace tripath.Handlers
{
    public class UpsertCustomerDetailAddressHandler : IRequestHandler<UpsertCustomerDetailAddressCommand, CustomerDetailAddress>
    {
        private readonly ICustomerDetailAddressRepository _repository;
        private readonly ICustomerMasterRepository _customerRepo; // For validating CustomerId

        public UpsertCustomerDetailAddressHandler(
            ICustomerDetailAddressRepository repository,
            ICustomerMasterRepository customerRepo)
        {
            _repository = repository;
            _customerRepo = customerRepo;
        }

        public async Task<CustomerDetailAddress> Handle(UpsertCustomerDetailAddressCommand request, CancellationToken cancellationToken)
        {
            var customer = request.Customer;
            if (CustomerValidationHelper.IsCustomerDetailAddressEmpty(customer))
                throw new ApiValidationException("At least one editable field is required.");

            //  Validate CustomerId
            var isCustomerValid = await _customerRepo.ExistsAsync(customer.CustomerId!);
            if (!isCustomerValid)
                throw new ApiValidationException("Invalid CustomerId.");


            if (string.IsNullOrEmpty(customer.CustomerDetailAddressId))
            {
                // Case: New entry
                customer.CustomerDetailAddressEntryDate = DateTime.UtcNow;
                customer.CustomerDetailAddressCreatedBy = request.CustomerUpdatedBy;

                await _repository.CreateAsync(customer);
            }
            else
            {
                // Validate CustomerDetailAddressId + CustomerId combo
                var existing = await _repository.GetByCustomerIdAndAddressIdAsync(customer.CustomerId!, customer.CustomerDetailAddressId);
                if (existing == null)
                {
                    throw new ApiValidationException("Invalid CustomerDetailAddress Id for given CustomerId.");

                }
                customer.CustomerUpdatedBy = request.CustomerUpdatedBy;

                await _repository.UpdateAsync(customer.CustomerDetailAddressId, customer);
            }

            return customer;
        }



    }

}

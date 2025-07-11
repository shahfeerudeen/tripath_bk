using MediatR;
using tripath.Commands;
using tripath.Exceptions;
using tripath.Models;
using tripath.Repositories;
using tripath.Utils;

namespace tripath.Handlers
{
    public class UpsertCustomerConsigneeHandler : IRequestHandler<UpsertCustomerConsigneeCommand, CustomerConsignee>
    {
        private readonly ICustomerConsigneeRepository _repository;
        private readonly ICustomerMasterRepository _customerRepo;

        public UpsertCustomerConsigneeHandler(
            ICustomerConsigneeRepository repository,
            ICustomerMasterRepository customerRepo)
        {
            _repository = repository;
            _customerRepo = customerRepo;
        }

        public async Task<CustomerConsignee> Handle(UpsertCustomerConsigneeCommand request, CancellationToken cancellationToken)
        {
            var consignee = request.Consignee;

            // 1. Validate empty payload
            if (CustomerValidationHelper.IsCustomerConsigneeEmpty(consignee))
                throw new ApiValidationException("At least one editable field is required.");

            // 2. Validate CustomerId exists
            var isCustomerValid = await _customerRepo.ExistsAsync(consignee.CustomerId!);
            if (!isCustomerValid)
                throw new ApiValidationException("Invalid CustomerId.");

            // 3. Update or Create
            if (string.IsNullOrEmpty(consignee.CustomerConsigneeId))
            {
                // New
                consignee.CustomerConsigneeEntryDate = DateTime.UtcNow;
                consignee.CustomerConsigneeCreatedBy = request.CustomerUpdatedBy;
                consignee.CustomerConsigneeStatus = "Y";

                await _repository.CreateAsync(consignee);
            }
            else
            {
                // Update
                var existing = await _repository.GetByCustomerIdAndConsigneeIdAsync(consignee.CustomerId!, consignee.CustomerConsigneeId);
                if (existing == null)
                    throw new ApiValidationException("Invalid CustomerConsigneeId for given CustomerId.");

                consignee.CustomerUpdatedBy = request.CustomerUpdatedBy;

                await _repository.UpdateAsync(consignee.CustomerConsigneeId, consignee);
            }

            return consignee;
        }
    }
}

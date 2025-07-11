using MediatR;
using tripath.Commands;
using tripath.Exceptions;
using tripath.Models;
using tripath.Repositories;
using tripath.Utils;

namespace tripath.Handlers
{
    public class UpsertCustomerContactHandler : IRequestHandler<UpsertCustomerContactCommand, CustomerContact>
    {
        private readonly ICustomerContactRepository _repository;
        private readonly ICustomerMasterRepository _customerRepo;

        public UpsertCustomerContactHandler(
            ICustomerContactRepository repository,
            ICustomerMasterRepository customerRepo)
        {
            _repository = repository;
            _customerRepo = customerRepo;
        }

        public async Task<CustomerContact> Handle(UpsertCustomerContactCommand request, CancellationToken cancellationToken)
        {
            var contact = request.CustomerContact;

            if (CustomerValidationHelper.IsCustomerContactEmpty(contact))
                throw new ApiValidationException("At least one editable field is required.");

            // Validate CustomerId
            var isCustomerValid = await _customerRepo.ExistsAsync(contact.CustomerId!);
            if (!isCustomerValid)
                throw new ApiValidationException("Invalid CustomerId.");

            if (string.IsNullOrEmpty(contact.CustomerContactId))
            {
                // New Contact
                contact.CustomerContactEntryDate = DateTime.UtcNow;
                contact.CustomerContactCreatedBy = request.CustomerUpdatedBy;
                await _repository.CreateAsync(contact);
            }
            else
            {
                // Existing Contact
                var existing = await _repository.GetByIdAsync(contact.CustomerContactId);
                if (existing == null)
                    throw new ApiValidationException("Invalid CustomerContact Id for given CustomerId.");

                contact.CustomerUpdatedBy = request.CustomerUpdatedBy;
                contact.CustomerContactUpdateDate = DateTime.UtcNow;

                await _repository.UpdateAsync(contact.CustomerContactId, contact);
            }

            return contact;
        }
    }
}

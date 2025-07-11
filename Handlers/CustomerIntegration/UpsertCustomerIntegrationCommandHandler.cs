using MediatR;
using tripath.Exceptions;
using tripath.Models;
using tripath.Repositories;
using tripath.Utils;

public class UpsertCustomerIntegrationCommandHandler : IRequestHandler<UpsertCustomerIntegrationCommand, CustomerIntegration>
{
    private readonly ICustomerIntegrationRepository _repository;
    private readonly ICustomerMasterRepository _customerRepo;

    public UpsertCustomerIntegrationCommandHandler(
        ICustomerIntegrationRepository repository,
        ICustomerMasterRepository customerRepo)
    {
        _repository = repository;
        _customerRepo = customerRepo;
    }

    public async Task<CustomerIntegration> Handle(UpsertCustomerIntegrationCommand request, CancellationToken cancellationToken)
    {
        var integration = request.Integration;

        if (CustomerValidationHelper.IsCustomerIntegrationEmpty(integration))
            throw new ApiValidationException("At least one editable field is required.");

        // Validate CustomerId
        var isCustomerValid = await _customerRepo.ExistsAsync(integration.CustomerId!);
        if (!isCustomerValid)
            throw new ApiValidationException("Invalid CustomerId.");

        if (string.IsNullOrEmpty(integration.CustomerIntegrationId))
        {
            // New document
            integration.CustomIntegrationEntryDate = DateTime.UtcNow;
            integration.CustomerIntegrationCreatedBy = request.CustomerUpdatedBy;
            integration.CustomerIntegrationStatus = "Y";

            await _repository.CreateAsync(integration);
        }
        else
        {
            // Update flow - validate ID
            var existing = await _repository.GetByIdAsync(integration.CustomerIntegrationId);
            if (existing == null)
                throw new ApiValidationException("Invalid CustomerIntegrationId for given CustomerId.");

            integration.CustomerUpdatedBy = request.CustomerUpdatedBy;
            await _repository.UpdateAsync(integration.CustomerIntegrationId, integration);
        }

        return integration;
    }
}

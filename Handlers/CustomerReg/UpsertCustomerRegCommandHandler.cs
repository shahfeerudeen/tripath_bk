using MediatR;
using tripath.Exceptions;
using tripath.Models;
using tripath.Repositories;
using tripath.Utils;

public class UpsertCustomerRegCommandHandler : IRequestHandler<UpsertCustomerRegCommand, CustomerReg>
{
    private readonly ICustomerRegRepository _repository;
    private readonly ICustomerMasterRepository _customerRepo;

    public UpsertCustomerRegCommandHandler(
        ICustomerRegRepository repository,
        ICustomerMasterRepository customerRepo)
    {
        _repository = repository;
        _customerRepo = customerRepo;
    }

    public async Task<CustomerReg> Handle(UpsertCustomerRegCommand request, CancellationToken cancellationToken)
    {
        var reg = request.CustomerReg;

        if (CustomerValidationHelper.IsCustomerRegEmpty(reg))
            throw new ApiValidationException("At least one editable field is required.");

        var isCustomerValid = await _customerRepo.ExistsAsync(reg.CustomerId!);
        if (!isCustomerValid)
            throw new ApiValidationException("Invalid CustomerId.");

        if (string.IsNullOrEmpty(reg.CustomerRegistrationId))
        {
            reg.CustomerRegCreateddBy = request.CustomerUpdatedBy;
            reg.CustomerRegistrationStatus = "Y";
            reg.CustomerRegistrationEntryDate = DateTime.UtcNow;

            await _repository.CreateAsync(reg);
        }
        else
        {
            var existing = await _repository.GetByCustomerIdAndRegIdAsync(reg.CustomerId!, reg.CustomerRegistrationId);
            if (existing == null)
                throw new ApiValidationException("Invalid CustomerRegistrationId for given CustomerId.");

            reg.CustomerUpdatedBy = request.CustomerUpdatedBy;
            await _repository.UpdateAsync(reg.CustomerRegistrationId, reg);
        }

        return reg;
    }
}

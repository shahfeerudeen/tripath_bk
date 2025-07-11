using MediatR;
using tripath.Commands;
using tripath.Models;
using tripath.Repositories;

namespace tripath.Handlers;

public class UpdateCustomerMasterHandler : IRequestHandler<UpdateCustomerMasterCommand, CustomerMaster?>
{
    private readonly ICustomerMasterRepository _repository;

    public UpdateCustomerMasterHandler(ICustomerMasterRepository repository)
    {
        _repository = repository;
    }

    public async Task<CustomerMaster?> Handle(UpdateCustomerMasterCommand request, CancellationToken cancellationToken)
    {
        return await _repository.PartialUpdateAsync(request.CustomerId, request.UpdatedCustomer, request.UpdatedBy);
    }
}


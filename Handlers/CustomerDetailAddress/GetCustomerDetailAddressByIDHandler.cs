using MediatR;
using tripath.Models;
using tripath.Queries;
using tripath.Repositories;

namespace tripath.Handlers
{
    public class GetCustomerDetailAddressByIdHandler : IRequestHandler<GetCustomerDetailAddressByIdQuery, CustomerDetailAddress>
    {
        private readonly ICustomerDetailAddressRepository _repository;

        public GetCustomerDetailAddressByIdHandler(ICustomerDetailAddressRepository repository)
        {
            _repository = repository;
        }

     public async Task<CustomerDetailAddress> Handle(GetCustomerDetailAddressByIdQuery request, CancellationToken cancellationToken)
{
    return await _repository.GetByIdAsync(request.CustomerDetailAddressId);
}
    }
}

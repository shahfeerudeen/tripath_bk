using MediatR;
using tripath.Models;
using tripath.Queries;
using tripath.Repositories;

namespace tripath.Handlers
{
    public class GetCustomerDetailAddressesHandler : IRequestHandler<GetCustomerDetailAddressesQuery, List<CustomerDetailAddress>>
    {
        private readonly ICustomerDetailAddressRepository _repository;

        public GetCustomerDetailAddressesHandler(ICustomerDetailAddressRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CustomerDetailAddress>> Handle(GetCustomerDetailAddressesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByCustomerIdAsync(request.CustomerId);
        }
    }
}

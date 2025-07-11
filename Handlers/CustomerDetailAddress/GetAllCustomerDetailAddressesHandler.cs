using MediatR;
using tripath.Models;
using tripath.Queries;
using tripath.Repositories;

namespace tripath.Handlers
{
    public class GetAllCustomerDetailAddressesHandler : IRequestHandler<GetAllCustomerDetailAddressesQuery, List<CustomerDetailAddress>>
    {
        private readonly ICustomerDetailAddressRepository _repository;

        public GetAllCustomerDetailAddressesHandler(ICustomerDetailAddressRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CustomerDetailAddress>> Handle(GetAllCustomerDetailAddressesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}

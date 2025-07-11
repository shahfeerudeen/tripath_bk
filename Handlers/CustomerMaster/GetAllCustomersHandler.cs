using MediatR;
using tripath.Models;
using tripath.Queries.Customer;
using tripath.Repositories;

namespace tripath.Handlers.CustomerHandler
{
    public class GetAllCustomersHandler
        : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerWithAddressResponse>>
    {
        private readonly ICustomerMasterRepository _repository;

        public GetAllCustomersHandler(ICustomerMasterRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CustomerWithAddressResponse>> Handle(
            GetAllCustomersQuery request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetAllAsync();
        }
    }
}

using MediatR;
using tripath.Models;
using tripath.Queries.Customer;
using tripath.Repositories;

namespace tripath.Handlers.CustomerHandler
{
    public class GetCustomerByIdHandler
        : IRequestHandler<GetCustomerByIdQuery, CustomerWithAddressResponse?>
    {
        private readonly ICustomerMasterRepository _repository;

        public GetCustomerByIdHandler(ICustomerMasterRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerWithAddressResponse?> Handle(
            GetCustomerByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetByIdWithAddressAsync(request.Id);
        }
    }
}

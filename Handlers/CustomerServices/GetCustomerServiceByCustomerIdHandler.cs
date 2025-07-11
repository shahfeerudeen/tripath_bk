using MediatR;
using tripath.Models;
using tripath.Queries;
using tripath.Repositories;

namespace tripath.Handlers
{
    public class GetCustomerServiceByCustomerIdHandler : IRequestHandler<GetCustomerServiceByCustomerIdQuery, CustomerServices>
    {
        private readonly ICustomerServiceRepository _repository;

        public GetCustomerServiceByCustomerIdHandler(ICustomerServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerServices> Handle(GetCustomerServiceByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByCustomerIdAsync(request.CustomerId);
        }
    }
}

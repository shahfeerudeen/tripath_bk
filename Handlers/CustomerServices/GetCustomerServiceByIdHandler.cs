using MediatR;
using tripath.Models;
using tripath.Queries;
using tripath.Repositories;

namespace tripath.Handlers
{
    public class GetCustomerServiceByIdHandler : IRequestHandler<GetCustomerServiceByIdQuery, CustomerServices>
    {
        private readonly ICustomerServiceRepository _repository;

        public GetCustomerServiceByIdHandler(ICustomerServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerServices> Handle(GetCustomerServiceByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.CustomerServiceId);
        }
    }
}

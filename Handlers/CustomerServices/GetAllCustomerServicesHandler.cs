using MediatR;
using tripath.Models;
using tripath.Queries;
using tripath.Repositories;

namespace tripath.Handlers
{
    public class GetAllCustomerServicesHandler : IRequestHandler<GetAllCustomerServicesQuery, IEnumerable<CustomerServices>>
    {
        private readonly ICustomerServiceRepository _repository;

        public GetAllCustomerServicesHandler(ICustomerServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CustomerServices>> Handle(GetAllCustomerServicesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}

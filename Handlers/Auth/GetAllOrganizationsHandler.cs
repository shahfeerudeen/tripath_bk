using System.Threading;
using System.Threading.Tasks;
using MediatR;
using tripath.Commands;
using tripath.Models;
using tripath.Queries;
using tripath.Repositories;

namespace tripath.Handlers
{
    public class GetAllOrganizationsHandler
        : IRequestHandler<GetAllOrganizationsQuery, List<OrganizationMaster>>
    {
        private readonly IOrganizationRepository _repository;

        public GetAllOrganizationsHandler(IOrganizationRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<OrganizationMaster>> Handle(
            GetAllOrganizationsQuery request,
            CancellationToken cancellationToken
        )
        {
            return await _repository.GetAllAsync();
        }
    }
}

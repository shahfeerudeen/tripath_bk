using MediatR;
using tripath.Models;

namespace tripath.Queries
{
    public class GetAllOrganizationsQuery : IRequest<List<OrganizationMaster>> { }
}

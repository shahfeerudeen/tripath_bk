using MongoDB.Driver;
using tripath.Models;

namespace tripath.Repositories
{
    public interface IOrganizationRepository
    {
        Task<List<OrganizationMaster>> GetAllAsync();

        Task<OrganizationMaster?> GetByNameAsync(string organizationName);

        Task<OrganizationMaster?> GetByIdAsync(string organizationId);
    }
}

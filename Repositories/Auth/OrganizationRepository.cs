using Data;
using MongoDB.Bson;
using MongoDB.Driver;
using tripath.Models;
using tripath.Services;

namespace tripath.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly IMongoCollection<OrganizationMaster> _collection;

        private readonly IFieldAliasService _alias;

        public OrganizationRepository(IMongoDatabase context, IFieldAliasService alias)
        {
            _collection = context.GetCollection<OrganizationMaster>("OrganizationMaster");
            _alias = alias;
        }

        public async Task<List<OrganizationMaster>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        /*  public async Task<OrganizationMaster?> GetByNameAsync(string organizationName)
          {
              return await _collection
                  .Find(x => x.OrganizationName.ToLower() == organizationName.ToLower())
                  .FirstOrDefaultAsync();
          } */
        public async Task<OrganizationMaster?> GetByNameAsync(string organizationName)
        {
            var orgNameField = _alias.GetField("OrganizationMaster", "OrganizationName").Trim();

            var filter = Builders<OrganizationMaster>.Filter
                .Eq(orgNameField, organizationName.ToLower());

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        /* public async Task<OrganizationMaster?> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        } */
        public async Task<OrganizationMaster?> GetByIdAsync(string id)
        {
            var idField = _alias.GetField("OrganizationMaster", "Id").Trim(); // Usually maps to "_id"

            var filter = Builders<OrganizationMaster>.Filter.Eq(idField, ObjectId.Parse(id));

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }  

    }
}

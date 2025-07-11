using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Tripath_Logistics_BE.Models.ExportJob;

namespace Tripath_Logistics_BE.Repositories.ExportJob
{
    public class ExportEntityRepository: IExportEntityRepository
{
    private readonly IMongoCollection<ExportEntity> _collection;

    public ExportEntityRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<ExportEntity>("ExportEntity");
    }

    public async Task<string> UpsertExportEntityAsync(ExportEntity entity)
    {
        if (string.IsNullOrEmpty(entity.ExportEntityId))
        {
            entity.ExportStatus = "Active";
            entity.ExportEntryDate = DateTime.UtcNow;
            entity.ExportUpdateDate = DateTime.UtcNow;
            await _collection.InsertOneAsync(entity);
            return entity.ExportEntityId;
        }
        else
        {
            entity.ExportUpdateDate = DateTime.UtcNow;
            var filter = Builders<ExportEntity>.Filter.Eq(x => x.ExportEntityId, entity.ExportEntityId);
            await _collection.ReplaceOneAsync(filter, entity);
            return entity.ExportEntityId;
        }
    }

    public async Task<List<ExportEntity>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<ExportEntity> GetByIdAsync(string id)
    {
        return await _collection.Find(x => x.ExportEntityId == id).FirstOrDefaultAsync();
    }
}
   
}
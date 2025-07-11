using MongoDB.Driver;
using System.Threading.Tasks;

public class ExporterMasterRepository : IExporterMasterRepository
{
    private readonly IMongoCollection<ExporterMaster> _masterCollection;

    public ExporterMasterRepository(IMongoDatabase database)
    {
        _masterCollection = database.GetCollection<ExporterMaster>("ExporterMaster");
    }

    public async Task<string> CreateExporterMasterAsync(ExporterMaster master)
    {
        await _masterCollection.InsertOneAsync(master);
        return master.ExporterMasterId;
    }
     public async Task<ExporterMaster?> GetByIdAsync(string id)
        {
            return await _masterCollection.Find(x => x.ExporterMasterId == id).FirstOrDefaultAsync();
        }
}

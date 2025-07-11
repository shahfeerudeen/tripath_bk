using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using tripath.Models.ExportJob;

public class ExporterRepository : IExporterRepository
{
    private readonly IMongoCollection<Exporter> _exporterCollection;

    public ExporterRepository(IMongoDatabase database)
    {
        _exporterCollection = database.GetCollection<Exporter>("Exporter");
    }

    public async Task<string> CreateExporterAsync(Exporter exporter)
    {
        await _exporterCollection.InsertOneAsync(exporter);
        return exporter.ExporterGeneralId!;
    }

    public async Task<Exporter?> GetByIdAsync(string id)
    {
        // Optional: Add safety check
        if (!ObjectId.TryParse(id, out _))
            return null;

        return await _exporterCollection
            .Find(x => x.ExporterGeneralId == id)
            .FirstOrDefaultAsync();
    }
}

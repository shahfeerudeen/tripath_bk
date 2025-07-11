public interface IExporterMasterRepository
{
    Task<string> CreateExporterMasterAsync(ExporterMaster master);
    Task<ExporterMaster?> GetByIdAsync(string id);
}

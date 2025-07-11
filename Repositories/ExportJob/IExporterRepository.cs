public interface IExporterRepository
{
    Task<string> CreateExporterAsync(Exporter exporter);

     Task<Exporter?> GetByIdAsync(string id);
}

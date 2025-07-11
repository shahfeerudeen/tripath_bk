using MediatR;
using tripath.Models.ExportJob;
using tripath.Queries.ExportJob;
using tripath.Repositories;

namespace tripath.Handlers.ExportJob
{
    public class GetCombinedExporterHandler : IRequestHandler<GetCombinedExporterQuery, CombainedExporterRequest>
    {
        private readonly IExporterRepository _exporterRepository;
        private readonly IExporterMasterRepository _exporterMasterRepository;

        public GetCombinedExporterHandler(
            IExporterRepository exporterRepository,
            IExporterMasterRepository exporterMasterRepository)
        {
            _exporterRepository = exporterRepository;
            _exporterMasterRepository = exporterMasterRepository;
        }

public async Task<CombainedExporterRequest> Handle(GetCombinedExporterQuery request, CancellationToken cancellationToken)
{
    Console.WriteLine($"📥 Handler: Received ID = {request.ExporterGeneralId}");

    var exporter = await _exporterRepository.GetByIdAsync(request.ExporterGeneralId);
    if (exporter == null)
    {
        Console.WriteLine("❌ Exporter not found.");
        throw new Exception("Exporter not found");
    }

    Console.WriteLine($"✅ Exporter found. ExporterMasterId: {exporter.ExporterMasterId}");

    var exporterMaster = await _exporterMasterRepository.GetByIdAsync(exporter.ExporterMasterId);
    if (exporterMaster == null)
    {
        Console.WriteLine("⚠ ExporterMaster not found.");
    }
    else
    {
        Console.WriteLine("✅ ExporterMaster found.");
    }

    return new CombainedExporterRequest
    {
        Exporter = exporter,
        ExporterMaster = exporterMaster
    };
}

    }
}

using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class CreateCombainedExporterHandler : IRequestHandler<CreateCombainedExporterCommand, string>
{
    private readonly IExporterRepository _exporterRepository;
    private readonly IExporterMasterRepository _masterRepository;

    public CreateCombainedExporterHandler(
        IExporterRepository exporterRepository,
        IExporterMasterRepository masterRepository)
    {
        _exporterRepository = exporterRepository;
        _masterRepository = masterRepository;
    }

    public async Task<string> Handle(CreateCombainedExporterCommand command, CancellationToken cancellationToken)
    {
        // Step 1: Save ExporterMaster and get ID
        var masterId = await _masterRepository.CreateExporterMasterAsync(command.Request.ExporterMaster);

        // Step 2: Assign the generated ExporterMasterId to the Exporter model
        command.Request.Exporter.ExporterMasterId = masterId;

        // Step 3: Save Exporter
        var exporterId = await _exporterRepository.CreateExporterAsync(command.Request.Exporter);

        return exporterId;
    }
}

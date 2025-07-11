using MediatR;
using tripath.Models.ExportJob;

namespace tripath.Queries.ExportJob
{
    public class GetCombinedExporterQuery : IRequest<CombainedExporterRequest>
    {
        public string ExporterGeneralId { get; set; }

        public GetCombinedExporterQuery(string exporterGeneralId)
        {
            ExporterGeneralId = exporterGeneralId;
        }
    }
}

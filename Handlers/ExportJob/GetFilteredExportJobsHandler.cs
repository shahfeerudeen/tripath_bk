using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using tripath.Queries.ExportJob;
using tripath.Repositories.ExportJob;
using ExportJobModel = tripath.Models.ExportJob.ExportJob;

namespace tripath.Handlers.ExportJob
{
    public class GetFilteredExportJobsHandler : IRequestHandler<GetFilteredExportJobsQuery, List<ExportJobModel>>
    {
        private readonly IExportJobRepository _repository;

        public GetFilteredExportJobsHandler(IExportJobRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ExportJobModel>> Handle(GetFilteredExportJobsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetFilteredExportJobsAsync(request.Jobno, request.ExporterDate, request.Exporter);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Tripath_Logistics_BE.Models.ExportJob;
using Tripath_Logistics_BE.Queries.ExportJob;
using Tripath_Logistics_BE.Repositories.ExportJob;

namespace Tripath_Logistics_BE.Handlers.ExportJob
{
   public class GetAllExportEntitiesHandler : IRequestHandler<GetAllExportEntitiesQuery, List<ExportEntity>>
{
    private readonly IExportEntityRepository _repository;

    public GetAllExportEntitiesHandler(IExportEntityRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ExportEntity>> Handle(GetAllExportEntitiesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
}
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
   
public class GetExportEntityByIdHandler : IRequestHandler<GetExportEntityByIdQuery, ExportEntity>
{
    private readonly IExportEntityRepository _repository;

    public GetExportEntityByIdHandler(IExportEntityRepository repository)
    {
        _repository = repository;
    }

    public async Task<ExportEntity> Handle(GetExportEntityByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}
}
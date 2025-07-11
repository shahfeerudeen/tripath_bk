using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Tripath_Logistics_BE.Commands.ExportJob;
using Tripath_Logistics_BE.Models.ExportJob;
using Tripath_Logistics_BE.Repositories.ExportJob;

namespace Tripath_Logistics_BE.Handlers.ExportJob
{
   public class UpsertExportEntityHandler : IRequestHandler<UpsertExportEntityCommand, string>
{
    private readonly IExportEntityRepository _repository;

    public UpsertExportEntityHandler(IExportEntityRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> Handle(UpsertExportEntityCommand request, CancellationToken cancellationToken)
    {
        var entity = new ExportEntity
        {
            ExportEntityId = request.ExportEntityId,
            ExportMasterId = request.ExportMasterId,
            ExportEntityType = request.ExportEntityType,
            ExportEntityName = request.ExportEntityName,
            ExportEntityCountry = request.ExportEntityCountry,
            ExportState = request.ExportState,
            ExportCity = request.ExportCity,
            ExportEntityPostalCode = request.ExportEntityPostalCode,
            ExportEntityAddress = request.ExportEntityAddress,
            ExportEntityTelephone = request.ExportEntityTelephone,
            ExportEntityFax = request.ExportEntityFax,
            ExportEntityContactPerson = request.ExportEntityContactPerson,
            ExportEntityEmail = request.ExportEntityEmail,
            ExportEntityMobile = request.ExportEntityMobile
        };

        return await _repository.UpsertExportEntityAsync(entity);
    }
}
}
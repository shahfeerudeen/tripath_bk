using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Tripath_Logistics_BE.Commands.ExportJob
{
    public class UpsertExportEntityCommand : IRequest<String>
    {
    public string? ExportEntityId { get; set; }
    public string? ExportMasterId { get; set; }
    public string? ExportEntityType { get; set; }
    public string? ExportEntityName { get; set; }
    public string? ExportEntityCountry { get; set; }
    public string? ExportState { get; set; }
    public string? ExportCity { get; set; }
    public string? ExportEntityPostalCode { get; set; }
    public string? ExportEntityAddress { get; set; }
    public string? ExportEntityTelephone { get; set; }
    public string? ExportEntityFax { get; set; }
    public string? ExportEntityContactPerson { get; set; }
    public string? ExportEntityEmail { get; set; }
    public string? ExportEntityMobile { get; set; }
        
    }
}
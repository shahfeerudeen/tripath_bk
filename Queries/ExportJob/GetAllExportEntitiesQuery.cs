using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Tripath_Logistics_BE.Models.ExportJob;

namespace Tripath_Logistics_BE.Queries.ExportJob
{
    public class GetAllExportEntitiesQuery: IRequest<List<ExportEntity>> 
    {
        
    }
}
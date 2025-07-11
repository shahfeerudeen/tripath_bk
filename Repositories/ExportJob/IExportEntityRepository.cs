using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tripath_Logistics_BE.Models.ExportJob;

namespace Tripath_Logistics_BE.Repositories.ExportJob
{
    public interface IExportEntityRepository
    {
                Task<string> UpsertExportEntityAsync(ExportEntity entity);
                Task<List<ExportEntity>> GetAllAsync();
                Task<ExportEntity> GetByIdAsync(string id);
    }
}
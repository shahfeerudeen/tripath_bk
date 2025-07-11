using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExportJobModel = tripath.Models.ExportJob.ExportJob;

namespace tripath.Repositories.ExportJob
{
    public interface IExportJobRepository
    {
        Task<List<ExportJobModel>> GetFilteredExportJobsAsync(string? jobno, DateTime? exporterDate, string? exporter);
    }
}

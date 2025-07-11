using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using ExportJobModel = tripath.Models.ExportJob.ExportJob;

namespace tripath.Repositories.ExportJob
{
    public class ExportJobRepository : IExportJobRepository
    {
        private readonly IMongoCollection<ExportJobModel> _collection;

        public ExportJobRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<ExportJobModel>("ExportJob");
        }

        public async Task<List<ExportJobModel>> GetFilteredExportJobsAsync(string? jobno, DateTime? exporterDate, string? exporter)
        {
            var filterBuilder = Builders<ExportJobModel>.Filter;
            var filter = filterBuilder.Eq(x => x.ExportStatus, "Y");

            if (!string.IsNullOrWhiteSpace(jobno))
                filter &= filterBuilder.Eq(x => x.JobNo, jobno);

            if (exporterDate.HasValue)
            {
                var start = exporterDate.Value.Date;
                var end = start.AddDays(1);
                filter &= filterBuilder.Gte(x => x.ExporterDate, start) &
                          filterBuilder.Lt(x => x.ExporterDate, end);
            }

            if (!string.IsNullOrWhiteSpace(exporter))
                filter &= filterBuilder.Eq(x => x.ExporterOrganizationName, exporter);

            return await _collection.Find(filter).ToListAsync();
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using ExportJobModel = tripath.Models.ExportJob.ExportJob;

namespace tripath.Queries.ExportJob
{
    public class GetFilteredExportJobsQuery : IRequest<List<ExportJobModel>>
    {
        public string? Jobno { get; set; }
        public DateTime? ExporterDate { get; set; }
        public string? Exporter { get; set; }
    }
}

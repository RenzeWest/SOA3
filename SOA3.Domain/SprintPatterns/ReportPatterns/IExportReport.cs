namespace SOA3.Domain.SprintPatterns.ReportPatterns
{
    public interface IExportReport
    {
        public Document ExportReport(ReportSettings settings);
    }
}

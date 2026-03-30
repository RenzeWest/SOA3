namespace SOA3.Domain.SprintPatterns.ReportPatterns
{
    public class SprintReport
    {
        private Guid _id = new Guid();
        private Sprint _sprint;
        private int _averageEffortPerDeveloper;
        private DateTime _generatedOn;
        private IExportReport exportReport;

        public SprintReport()
        {
            // Init IExportReport
        }

        public Document ExportReport(ReportSettings settings) => exportReport.ExportReport(settings);

        public void SetExportStrategy(IExportReport strategy)
        {
            exportReport = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }
    }
}

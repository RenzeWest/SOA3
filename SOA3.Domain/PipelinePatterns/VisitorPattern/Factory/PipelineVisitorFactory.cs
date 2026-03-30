namespace SOA3.Domain.PipelinePatterns.VisitorPattern.Factory
{
    public class PipelineVisitorFactory
    {
        public IPipelineVisitor PickPipelineVisitor(bool isDryRun) => isDryRun ? new DryRunVisitor() : new RunVisitor();
    }
}

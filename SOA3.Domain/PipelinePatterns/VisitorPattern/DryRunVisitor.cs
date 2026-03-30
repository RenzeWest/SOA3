using SOA3.Domain.PipelinePatterns.CompositePattern.Leafs;

namespace SOA3.Domain.PipelinePatterns.VisitorPattern
{
    public class DryRunVisitor : IPipelineVisitor
    {
        public void PipelineFailed(Exception exception) => Console.WriteLine($"[Dry Run] Failed with message:{exception.Message}"); // TODO: Should fail the sprint Ig

        public void VisitAnalysePipelineAction(AnalysePipelineAction action) => Console.WriteLine($"[Dry Run] Analyse action called: {action.Name}");

        public void VisitBuildPipelineAction(BuildPipelineAction action) => Console.WriteLine($"[Dry Run] Build action called: {action.Name}");

        public void VisitDeployPipelineAction(DeployPipelineAction action) => Console.WriteLine($"[Dry Run] Deploy action called: {action.Name}");

        public void VisitPackagePipelineAction(PackagePipelineAction action) => Console.WriteLine($"[Dry Run] Package action called: {action.Name}");

        public void VisitSourcePipelineAction(SourcePipelineAction action) => Console.WriteLine($"[Dry Run] Source action called: {action.Name}");

        public void VisitTestPipelineAction(TestPipelineAction action) => Console.WriteLine($"[Dry Run] Test action called: {action.Name}");

        public void VisitUtilityPipelineAction(UtilityPipelineAction action) => Console.WriteLine($"[Dry Run] Utility action called: {action.Name}");
    }
}

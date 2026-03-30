using SOA3.Domain.PipelinePatterns.CompositePattern.Leafs;

namespace SOA3.Domain.PipelinePatterns.VisitorPattern
{
    public class RunVisitor : IPipelineVisitor
    {
        public void PipelineFailed(Exception exception) => Console.WriteLine($"[Run] Failed with message:{exception.Message}"); // TODO: Should fail the sprint Ig

        public void VisitAnalysePipelineAction(AnalysePipelineAction action) => Console.WriteLine($"[Run] Analyse action called");

        public void VisitBuildPipelineAction(BuildPipelineAction action) => Console.WriteLine($"[Run] Build action called");

        public void VisitDeployPipelineAction(DeployPipelineAction action) => Console.WriteLine($"[Run] Deploy action called");

        public void VisitPackagePipelineAction(PackagePipelineAction action) => Console.WriteLine($"[Run] Package action called");

        public void VisitSourcePipelineAction(SourcePipelineAction action) => Console.WriteLine($"[Run] Source action called");

        public void VisitTestPipelineAction(TestPipelineAction action) => Console.WriteLine($"[Run] Test action called");

        public void VisitUtilityPipelineAction(UtilityPipelineAction action) => Console.WriteLine($"[Run] Utility action called");
    }
}

using SOA3.Domain.PipelinePatterns.CompositePattern.Leafs;

namespace SOA3.Domain.PipelinePatterns.VisitorPattern
{
    public interface IPipelineVisitor
    {
        public void VisitSourcePipelineAction(SourcePipelineAction action);
        public void VisitPackagePipelineAction(PackagePipelineAction action);
        public void VisitBuildPipelineAction(BuildPipelineAction action);
        public void VisitTestPipelineAction(TestPipelineAction action);
        public void VisitAnalysePipelineAction(AnalysePipelineAction action);
        public void VisitDeployPipelineAction(DeployPipelineAction action);
        public void VisitUtilityPipelineAction(UtilityPipelineAction action);
        public void PipelineFailed(Exception exception);
    }
}

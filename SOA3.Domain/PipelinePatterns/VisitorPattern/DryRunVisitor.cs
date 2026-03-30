using SOA3.Domain.PipelinePatterns.CompositePattern.Leafs;

namespace SOA3.Domain.PipelinePatterns.VisitorPattern
{
    public class DryRunVisitor : IPipelineVisitor
    {
        public void PipelineFailed(Exception exception)
        {
            throw new NotImplementedException();
        }

        public void VisitAnalysePipelineAction(AnalysePipelineAction action)
        {
            throw new NotImplementedException();
        }

        public void VisitBuildPipelineAction(BuildPipelineAction action)
        {
            throw new NotImplementedException();
        }

        public void VisitDeployPipelineAction(DeployPipelineAction action)
        {
            throw new NotImplementedException();
        }

        public void VisitPackagePipelineAction(PackagePipelineAction action)
        {
            throw new NotImplementedException();
        }

        public void VisitSourcePipelineAction(SourcePipelineAction action)
        {
            throw new NotImplementedException();
        }

        public void VisitTestPipelineAction(TestPipelineAction action)
        {
            throw new NotImplementedException();
        }

        public void VisitUtilityPipelineAction(UtilityPipelineAction action)
        {
            throw new NotImplementedException();
        }
    }
}

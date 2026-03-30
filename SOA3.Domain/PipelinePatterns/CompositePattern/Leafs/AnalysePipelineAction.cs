using SOA3.Domain.PipelinePatterns.VisitorPattern;

namespace SOA3.Domain.PipelinePatterns.CompositePattern.Leafs
{
    public class AnalysePipelineAction : PipelineActionComponent
    {
        public override void AcceptVisitor(IPipelineVisitor visitor)
        {
            visitor.VisitAnalysePipelineAction(this);
        }
    }
}

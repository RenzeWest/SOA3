using SOA3.Domain.PipelinePatterns.VisitorPattern;

namespace SOA3.Domain.PipelinePatterns.CompositePattern.Leafs
{
    public class SourcePipelineAction : PipelineActionComponent
    {
        public override void AcceptVisitor(IPipelineVisitor visitor)
        {
            visitor.VisitSourcePipelineAction(this);
        }
    }
}

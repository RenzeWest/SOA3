using SOA3.Domain.PipelinePatterns.VisitorPattern;

namespace SOA3.Domain.PipelinePatterns.CompositePattern.Leafs
{
    public class PackagePipelineAction : PipelineActionComponent
    {
        public override void AcceptVisitor(IPipelineVisitor visitor)
        {
            visitor.VisitPackagePipelineAction(this);
        }
    }
}

using SOA3.Domain.PipelinePatterns.VisitorPattern;

namespace SOA3.Domain.PipelinePatterns.CompositePattern
{
    public abstract class PipelineActionComponent
    {
        private Guid _id = Guid.NewGuid(); 
        public required  string Name { get; init; } // action
        
        public abstract void AcceptVisitor(IPipelineVisitor visitor);
    }
}

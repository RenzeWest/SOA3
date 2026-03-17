using SOA3.Domain.PipelinePatterns.VisitorPattern;

namespace SOA3.Domain.PipelinePatterns.CompositePattern
{
    public abstract class PipelineActionComponent
    {
        private Guid _id = Guid.NewGuid(); 
        private string _name;
        //      private ActionStatus _status

        public abstract void AcceptVisitor(IPipelineVisitor visitor);
    }
}

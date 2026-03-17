using SOA3.Domain.PipelinePatterns.VisitorPattern;

namespace SOA3.Domain.PipelinePatterns.CompositePattern
{
    public abstract class PipelineComposite : PipelineActionComponent
    {
        private List<PipelineActionComponent> _actions = new List<PipelineActionComponent>();
        public void Add(PipelineActionComponent actions) => _actions.Add(actions);
        public void Remove(PipelineActionComponent action) => _actions.Remove(action);
        public PipelineActionComponent GetComponent(int index) => _actions[index];
        public void AcceptVisitors(IPipelineVisitor visitor) => _actions.ForEach(action => action.AcceptVisitor(visitor));
    }
}

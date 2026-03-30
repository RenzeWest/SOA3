using SOA3.Domain.PipelinePatterns.CompositePattern;

namespace SOA3.Domain.PipelinePatterns
{
    public class DevelopmentPipeline
    {
        private Guid _id = Guid.NewGuid();
        private string _name;
        private bool _isAutomatic;
        private List<PipelineActionComponent> _actions = new List<PipelineActionComponent>();
        public void AddPipelineAction(PipelineActionComponent pipelineAction) => _actions.Add(pipelineAction);
        public void RemovePipelineAction(PipelineActionComponent pipelineAction) => _actions.Remove(pipelineAction);
    }
}

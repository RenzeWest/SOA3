using SOA3.Domain.SprintPatterns.SprintStatePattern;

namespace SOA3.Domain.SprintPatterns
{
    public class DeploymentSprint : Sprint
    {
        private readonly ISprintState _finalizedState;
        public DeploymentSprint() => _finalizedState = new ReleasingState(this);
        public override ISprintState FinalizedState => _finalizedState;
    }
}

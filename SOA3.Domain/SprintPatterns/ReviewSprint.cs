using SOA3.Domain.SprintPatterns.SprintStatePattern;

namespace SOA3.Domain.SprintPatterns
{
    public class ReviewSprint : Sprint
    {
        private readonly ISprintState _finalizedState;
        public ReviewSprint() => _finalizedState = new ReviewingState(this);
        public override ISprintState FinalizedState => _finalizedState;
    }
}

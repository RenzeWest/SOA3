namespace SOA3.Domain.SprintPatterns.SprintStatePattern
{
    public class ClosedState : ISprintState
    {
        private Sprint _sprint;

        public ClosedState(Sprint sprint) => _sprint = sprint;
        public void AddBacklogItem() => throw new InvalidOperationException();

        public void CloseSprint() => throw new InvalidOperationException();

        public void FinalizeSprint() => throw new InvalidOperationException();

        public void FinishSprint() => throw new InvalidOperationException();

        public void RetrySprint() => throw new InvalidOperationException();

        public void StartSprint() => throw new InvalidOperationException();

        public void UpdateSprint() => throw new InvalidOperationException();
        public void CancelSprint() => throw new InvalidOperationException();
    }
}

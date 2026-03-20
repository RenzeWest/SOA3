namespace SOA3.Domain.SprintPatterns.SprintStatePattern
{
    public class FinishedState : ISprintState
    {
        private Sprint _sprint;

        public FinishedState(Sprint sprint) => _sprint = sprint;
        public void CloseSprint() => throw new InvalidOperationException();

        public void FinalizeSprint()
        {
            Console.WriteLine("Finalizing sprint");
            _sprint.setState(_sprint.FinalizedState);
        }

        public void FinishSprint() => throw new InvalidOperationException();

        public void RetrySprint() => throw new InvalidOperationException();

        public void StartSprint() => throw new InvalidOperationException();
        public void CancelSprint() 
        {
            Console.WriteLine("Sprint is cancelled");
            _sprint.setState(_sprint.CancelledState);
        }
    }
}

namespace SOA3.Domain.SprintPatterns.SprintStatePattern
{
    public class CancelledState : ISprintState
    {
        private Sprint _sprint;

        public CancelledState(Sprint sprint) => _sprint = sprint;
        public void CloseSprint()
        {
            Console.WriteLine("Closing cancelled state");
            _sprint.setState(_sprint.ClosedState);
        }

        public void FinalizeSprint() => throw new InvalidOperationException();

        public void FinishSprint() => throw new InvalidOperationException();

        public void RetrySprint() => throw new InvalidOperationException();

        public void StartSprint() => throw new InvalidOperationException();
        public void CancelSprint() => throw new InvalidOperationException();
    }
}

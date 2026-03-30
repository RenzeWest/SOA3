namespace SOA3.Domain.SprintPatterns.SprintStatePattern
{
    public class InProgressState : ISprintState
    {
        private Sprint _sprint;

        public InProgressState(Sprint sprint) => _sprint = sprint;

        public void CloseSprint() => throw new InvalidOperationException();

        public void FinalizeSprint() => throw new InvalidOperationException();

        public void FinishSprint()
        {
            Console.WriteLine("Sprint is being finished");
            _sprint.setState(_sprint.FinishedState);
        }

        public void RetrySprint() => throw new InvalidOperationException();

        public void StartSprint() => throw new InvalidOperationException();
        public void CancelSprint() => throw new InvalidOperationException();
    }
}

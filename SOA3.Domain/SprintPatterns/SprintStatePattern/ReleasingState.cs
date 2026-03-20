namespace SOA3.Domain.SprintPatterns.SprintStatePattern
{
    public class ReleasingState : ISprintState
    {
        private Sprint _sprint;

        public ReleasingState(Sprint sprint) => _sprint = sprint;

        public void CloseSprint()
        {
            Console.WriteLine("Closing sprint with a deployment");
            // Start deployment
            // sprint.StartDeployment();
            // if successful deployment close
            _sprint.setState(_sprint.ClosedState);
            // or set back to finished and send notification
            _sprint.setState(_sprint.FinishedState);
        }

        public void FinalizeSprint() => throw new InvalidOperationException();

        public void FinishSprint() => throw new InvalidOperationException();

        public void RetrySprint()
        {
            Console.WriteLine("Retrying release again");
            // sprint.StartDeployment();
            //// if success
            _sprint.setState(_sprint.ClosedState);
            //// if fail
            // send notification
            _sprint.setState(_sprint.FinishedState);
        }

        public void StartSprint() => throw new InvalidOperationException();
        public void CancelSprint() => throw new InvalidOperationException();
    }
}

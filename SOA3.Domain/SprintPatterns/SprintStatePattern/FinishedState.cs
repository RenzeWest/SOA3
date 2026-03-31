using SOA3.Domain.NotificationPatterns;

namespace SOA3.Domain.SprintPatterns.SprintStatePattern
{
    public class FinishedState : ISprintState
    {
        private Sprint _sprint;
        private NotificationPublisher _publisher = new NotificationPublisher();

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
            var message = "Sprint is cancelled";
            Console.WriteLine(message);
            _publisher.NotifySubscribers(new Notification(DateTime.UtcNow, message));

            _sprint.setState(_sprint.CancelledState);
        }
    }
}

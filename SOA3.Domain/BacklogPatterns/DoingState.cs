using SOA3.Domain.NotificationPatterns;

namespace SOA3.Domain.BacklogPatterns
{
    public class DoingState : IBacklogItemState
    {
        private IBacklogItem _backlogItem;
        private NotificationPublisher _publisher = new NotificationPublisher();

        public DoingState(IBacklogItem backlogItem) => _backlogItem = backlogItem;
        public void StartWork() => throw new InvalidOperationException();
        public void MarkReadyForTesting()
        {
            var message = $"{_backlogItem.Title} is ready for testing";
            Console.WriteLine(message);
            _publisher.NotifySubscribers(new Notification(DateTime.UtcNow, message));

            _backlogItem.SetState(_backlogItem.ReadyForTestingState);
        }
        public void StartTesting() => throw new InvalidOperationException();
        public void ApproveTest() => throw new InvalidOperationException();
        public void AcceptDone() => throw new InvalidOperationException();
        public void RejectToDo() => throw new InvalidOperationException();
        public void RejectToReadyForTesting() => throw new InvalidOperationException();
    }
}

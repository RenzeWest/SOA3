namespace SOA3.Domain.BacklogPatterns
{
    public class TodoState : IBacklogItemState
    {
        private IBacklogItem _backlogItem;

        public TodoState(IBacklogItem backlogItem) => _backlogItem = backlogItem;

        public void StartWork()
        {
            // TODO: Check if person is already working on it
            Console.WriteLine($"Started working on backlog item {_backlogItem.Title}");
            _backlogItem.SetState(_backlogItem.DoingState);
        }

        public void MarkReadyForTesting() => throw new InvalidOperationException();
        public void StartTesting() => throw new InvalidOperationException();
        public void ApproveTest() => throw new InvalidOperationException();
        public void AcceptDone() => throw new InvalidOperationException();
        public void RejectToDo() => throw new InvalidOperationException();
        public void RejectToReadyForTesting() => throw new InvalidOperationException();
    }
}

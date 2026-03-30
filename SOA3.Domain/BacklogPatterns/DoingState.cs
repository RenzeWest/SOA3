namespace SOA3.Domain.BacklogPatterns
{
    public class DoingState : IBacklogItemState
    {
        private IBacklogItem _backlogItem;

        public DoingState(IBacklogItem backlogItem) => _backlogItem = backlogItem;
        public void StartWork() => throw new InvalidOperationException();
        public void MarkReadyForTesting()
        {
            Console.WriteLine($"{_backlogItem.Title} is ready for testing");
            _backlogItem.SetState(_backlogItem.ReadyForTestingState);
        }
        public void StartTesting() => throw new InvalidOperationException();
        public void ApproveTest() => throw new InvalidOperationException();
        public void AcceptDone() => throw new InvalidOperationException();
        public void RejectToDo() => throw new InvalidOperationException();
        public void RejectToReadyForTesting() => throw new InvalidOperationException();
    }
}

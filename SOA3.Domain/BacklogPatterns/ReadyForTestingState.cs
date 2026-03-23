namespace SOA3.Domain.BacklogPatterns
{
    public class ReadyForTestingState : IBacklogItemState
    {
        private IBacklogItem _backlogItem;

        public ReadyForTestingState(IBacklogItem backlogItem) => _backlogItem = backlogItem;
        public void StartWork() => throw new InvalidOperationException();
        public void MarkReadyForTesting() => throw new InvalidOperationException();
        public void StartTesting() 
        {
            Console.WriteLine($"{_backlogItem.Title} has started testing");
            _backlogItem.SetState(_backlogItem.TestingState);
        }
        public void ApproveTest() => throw new InvalidOperationException();
        public void AcceptDone() => throw new InvalidOperationException();
        public void RejectToDo() 
        {
            Console.WriteLine($"{_backlogItem.Title} Was rejected for testing and has been moved back to todo");
            _backlogItem.SetState(_backlogItem.TodoState);
        }
        public void RejectToReadyForTesting() => throw new InvalidOperationException();
    }
}

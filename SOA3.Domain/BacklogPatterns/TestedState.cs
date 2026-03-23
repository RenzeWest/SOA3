namespace SOA3.Domain.BacklogPatterns
{
    public class TestedState : IBacklogItemState
    {
        private IBacklogItem _backlogItem;

        public TestedState(IBacklogItem backlogItem) => _backlogItem = backlogItem;

        public void StartWork() => throw new InvalidOperationException();
        public void MarkReadyForTesting() => throw new InvalidOperationException();
        
        public void StartTesting() => throw new InvalidOperationException();
        public void ApproveTest() => throw new InvalidOperationException();
        public void AcceptDone()
        {
            Console.WriteLine($"{_backlogItem.Title} has been completed");
            _backlogItem.SetState(_backlogItem.DoneState);
        }
        public void RejectToDo()
        {
            Console.WriteLine($"{_backlogItem.Title} has been rejected and moved back to TODO");
            _backlogItem.SetState(_backlogItem.DoneState);
        }
        public void RejectToReadyForTesting()
        {
            Console.WriteLine($"{_backlogItem.Title} has been rejected and moved back to ready for testing");
            _backlogItem.SetState(_backlogItem.DoneState);
        }
    }
}

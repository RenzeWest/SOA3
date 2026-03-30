namespace SOA3.Domain.BacklogPatterns
{
    public class TestingState : IBacklogItemState
    {
        private IBacklogItem _backlogItem;

        public TestingState(IBacklogItem backlogItem) => _backlogItem = backlogItem;
        public void StartWork() => throw new InvalidOperationException();
        public void MarkReadyForTesting() => throw new InvalidOperationException();
        public void StartTesting() => throw new InvalidOperationException();
        public void ApproveTest()
        {
            Console.WriteLine($"{_backlogItem} has passed testing and entered the tested state");
            _backlogItem.SetState(_backlogItem.TestedState);
        }
        public void AcceptDone() => throw new InvalidOperationException();
        public void RejectToDo()
        {
            Console.WriteLine($"{_backlogItem.Title} Did not pass the tests and has been moved back to TODO");
            _backlogItem.SetState(_backlogItem.TodoState);
        }
        public void RejectToReadyForTesting()
        {
            _backlogItem.SetState(_backlogItem.ReadyForTestingState);
            Console.WriteLine("BacklogItem rejected from Testing to ReadyForTesting.");
        }
    }
}

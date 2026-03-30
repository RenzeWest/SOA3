namespace SOA3.Domain.BacklogPatterns
{
    public class DoneState : IBacklogItemState
    {
        private IBacklogItem _backlogItem;

        public DoneState(IBacklogItem backlogItem) => _backlogItem = backlogItem;
        public void StartWork() => throw new NotImplementedException();
        public void MarkReadyForTesting() => throw new NotImplementedException();
        public void StartTesting() => throw new NotImplementedException();
        public void ApproveTest() => throw new NotImplementedException();
        public void AcceptDone() => throw new NotImplementedException();
        public void RejectToDo() => throw new NotImplementedException();
        public void RejectToReadyForTesting() => throw new NotImplementedException();
    }
}

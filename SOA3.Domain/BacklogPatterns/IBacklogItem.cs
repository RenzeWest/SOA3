namespace SOA3.Domain.BacklogPatterns
{
    public interface IBacklogItem
    {
        public string Title { get; }
        public Person AssignedDeveloper { get; }
        public IBacklogItemState TodoState { get; }
        public IBacklogItemState DoingState { get; }
        public IBacklogItemState ReadyForTestingState { get; }
        public IBacklogItemState TestingState { get; }
        public IBacklogItemState TestedState { get; }
        public IBacklogItemState DoneState { get; }
        public void SetState(IBacklogItemState state);
        public IBacklogItemState GetState();
        public void StartWork();
        public void MarkReadyForTesting();
        public void StartTesting();
        public void ApproveTest();
        public void AcceptDone();
        public void RejectToDo();
        public void RejectToReadyForTesting();
    }
}

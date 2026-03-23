namespace SOA3.Domain.BacklogPatterns
{
    public class Activity : IBacklogItem
    {
        private Guid _id = Guid.NewGuid();
        public string Title { get; }
        private string _description;
        public Person AssignedDeveloper { get; }

        public Activity(Person assignedTo, string titel, string description)
        {
            TodoState = new TodoState(this);
            DoingState = new DoingState(this);
            ReadyForTestingState = new ReadyForTestingState(this);
            TestingState = new TestingState(this);
            TestedState = new TestedState(this);
            DoneState = new DoneState(this);
            _state = TodoState;

            AssignedDeveloper = assignedTo;
            Title = titel;
            _description = description;
        }

        public IBacklogItemState TodoState { get; }
        public IBacklogItemState DoingState { get; }
        public IBacklogItemState ReadyForTestingState { get; }
        public IBacklogItemState TestingState { get; }
        public IBacklogItemState TestedState { get; }
        public IBacklogItemState DoneState { get; }

        private IBacklogItemState _state;

        public void SetState(IBacklogItemState state) => _state = state;
        public IBacklogItemState GetState() => _state;
        public void StartWork() => _state.StartWork();
        public void MarkReadyForTesting() => _state.MarkReadyForTesting();
        public void StartTesting() => _state.StartTesting();
        public void ApproveTest() => _state.ApproveTest();
        public void AcceptDone() => _state.AcceptDone();
        public void RejectToDo() => _state.RejectToDo();
        public void RejectToReadyForTesting() => _state.RejectToReadyForTesting();

    }
}

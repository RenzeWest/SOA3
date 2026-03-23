using SOA3.Domain.ForumPattern;
using SOA3.Domain.SprintPatterns;

namespace SOA3.Domain.BacklogPatterns
{
    public class BacklogItem : IBacklogItem
    {
        private Guid _id;
        public string Title { get; }
        private string _description;
        private int _storyPoints;
        public Person AssignedDeveloper { get; }
        private Sprint _sprint;
        private List<DiscussionThread> _discussionThreads = new List<DiscussionThread>();
        private List<Activity> _activities = new List<Activity>();

        public IBacklogItemState TodoState { get; }
        public IBacklogItemState DoingState { get; }
        public IBacklogItemState ReadyForTestingState { get; }
        public IBacklogItemState TestingState { get; }
        public IBacklogItemState TestedState { get; }
        public IBacklogItemState DoneState { get; }

        private IBacklogItemState _state;
        
        public BacklogItem()
        {
            TodoState = new TodoState(this);
            DoingState = new DoingState(this);
            ReadyForTestingState = new ReadyForTestingState(this);
            TestingState = new TestingState(this);
            TestedState = new TestedState(this);
            DoneState = new DoneState(this);
            _state = TodoState;
        }

        public void AddDiscussionThread(DiscussionThread thread) => _discussionThreads.Add(thread);
        public void AddActivity(Activity activity) => _activities.Add(activity);
        public void SetState(IBacklogItemState state) => _state = state;
        public IBacklogItemState GetState() => _state;
        public void StartWork() => _state.StartWork();
        public void MarkReadyForTesting() => _state.MarkReadyForTesting();
        public void StartTesting() => _state.StartTesting();
        public void ApproveTest() => _state.ApproveTest();
        public void AcceptDone()
        {
            if (_activities.Any((activity) => activity.GetState().GetType() != DoneState.GetType()))
            {
                Console.WriteLine("This backlog item was not completed. One or more of the subactivities are not done.");
            }
            else _state.AcceptDone();
        }
        public void RejectToDo() => _state.RejectToDo();
        public void RejectToReadyForTesting() => _state.RejectToReadyForTesting();
    }
}
using SOA3.Domain.BacklogPatterns;
using SOA3.Domain.PipelinePatterns;
using SOA3.Domain.SprintPatterns.SprintStatePattern;

namespace SOA3.Domain.SprintPatterns
{
    public abstract class Sprint
    {
        private Guid _id = new Guid();
        private string _name;
        private DateTime _startDateTime;
        private DateTime _endDateTime;
        private Person _scrumMaster;
        private DevelopmentPipeline _pipeline;
        private Document _reviewSummaryDocument;
        private ReportSettings _reportSettings;
        private List<BacklogItem> _sprintBacklog = new List<BacklogItem>();

        public ISprintState CreatedState { get; }
        public ISprintState InProgressState { get; }
        public ISprintState FinishedState { get; }
        public abstract ISprintState FinalizedState { get; }
        public ISprintState CancelledState { get; }
        public ISprintState ClosedState { get; }

        private ISprintState _state;

        public Sprint()
        {
            CreatedState = new CreatedState(this);
            InProgressState = new InProgressState(this);    
            FinishedState = new FinishedState(this);
            CancelledState = new CancelledState(this);
            ClosedState = new ClosedState(this);

            _state = CreatedState;
        }

        public string Name => _name;
        public DateTime StartDateTime => _startDateTime;
        public DateTime EndDateTime => _endDateTime;
        public IReadOnlyList<BacklogItem> BacklogItems => _sprintBacklog.AsReadOnly();


        public void AddItemToBacklog(BacklogItem backlogItem) => _sprintBacklog.Add(backlogItem);
        public void setState(ISprintState state) => _state = state;
        public void StartSprint() => _state.StartSprint();
        public void FinishSprint() => _state.FinishSprint();
        public void FinalizeSprint() => _state.FinalizeSprint();
        public void RetryRelease() => _state.RetrySprint();
        public void CloseSprint() => _state.CloseSprint();
        public void CancelSprint() => _state.CancelSprint();
        public void UpdateDetails(string name, DateTime startDateTime, DateTime endDateTime)
        {
            if (_state == CreatedState)
            {
                _name = name;
                _startDateTime = startDateTime;
                _endDateTime = endDateTime;
            }
            else Console.WriteLine("Not allowed to change the details of the sprint");
        }
        public void AddBacklogItem(BacklogItem backlogItem)
        {
            if (backlogItem == null)
                throw new ArgumentNullException(nameof(backlogItem), "Backlog item cannot be null");

            if (_state == CreatedState)
                _sprintBacklog.Add(backlogItem);
            else
                Console.WriteLine("Not allowed to add an item to the backlog");
        }
    }
}

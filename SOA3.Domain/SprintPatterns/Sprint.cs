using SOA3.Domain.BacklogPatterns;

namespace SOA3.Domain.SprintPatterns
{
    public class Sprint
    {
        private Guid _id = new Guid();
        private string _name;
        private DateTime _startDateTime;
        private DateTime _endDateTime;
        // TYPE (?)
        private Person _scrumMaster;
        private DevelopmentPipeline _pipeline;
        private Document _reviewSummaryDocument;
        private ReportSettings _reportSettings;
        private List<BacklogItem> _sprintBacklog = new List<BacklogItem>();
        private SprintState _state;

        public Sprint()
        {
            // TODO: Initialize state
        }
        public void setState(SprintState state) => _state = state;
        public void StartSprint() => throw new NotImplementedException();
        public void FinishSprint() => throw new NotImplementedException();

        public void StartRelease() => throw new NotImplementedException();
        public void StartReview() => throw new NotImplementedException();
        public void RetryRelease() => throw new NotImplementedException();
        public void CloseSprint() => throw new NotImplementedException();
        public void UpdateDetails() => throw new NotImplementedException();
        public void AddBacklogItem() => throw new NotImplementedException();
    }
}

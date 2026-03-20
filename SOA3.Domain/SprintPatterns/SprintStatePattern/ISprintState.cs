using SOA3.Domain.BacklogPatterns;

namespace SOA3.Domain.SprintPatterns.SprintStatePattern
{
    public interface ISprintState
    {
        void StartSprint();
        void FinishSprint();
        void FinalizeSprint();
        void RetrySprint();
        void CloseSprint();
        void CancelSprint();
    }
}

using SOA3.Domain.BacklogPatterns;
using System.ComponentModel;

namespace SOA3.Domain.SprintPatterns.SprintStatePattern
{
    public class CreatedState : ISprintState
    {
        private Sprint _sprint;

        public CreatedState(Sprint sprint) => _sprint = sprint;

        public void CloseSprint() => throw new InvalidOperationException();

        public void FinalizeSprint() => throw new InvalidOperationException();

        public void FinishSprint() => throw new InvalidOperationException();

        public void RetrySprint() => throw new InvalidOperationException();

        public void StartSprint()
        {
            Console.WriteLine("Sprint was started");
            _sprint.setState(_sprint.InProgressState);
        }
        public void CancelSprint() => throw new InvalidOperationException();
    }
}

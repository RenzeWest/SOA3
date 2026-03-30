namespace SOA3.Domain.SprintPatterns.SprintStatePattern
{
    public class ReviewingState : ISprintState
    {
        private Sprint _sprint;

        public ReviewingState(Sprint sprint) => _sprint = sprint;

        public void CloseSprint()
        {
            Console.WriteLine("Closing sprint with a review");
            // TODO: Generate a review.
            // _sprint.generateReview ?
            _sprint.setState(_sprint.ClosedState);
        }

        public void FinalizeSprint() => throw new InvalidOperationException();

        public void FinishSprint() => throw new InvalidOperationException();

        public void RetrySprint() => throw new InvalidOperationException();
        public void StartSprint() => throw new InvalidOperationException();
        public void CancelSprint() => throw new InvalidOperationException();
    }
}

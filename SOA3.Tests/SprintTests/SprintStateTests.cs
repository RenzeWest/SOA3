using SOA3.Domain.BacklogPatterns;
using SOA3.Domain.SprintPatterns;
using SOA3.Domain.SprintPatterns.SprintStatePattern;
using System;
using System.IO;
using Xunit;

namespace SOA3.Tests.SprintPatterns
{
    [Collection("Sequential")]
    public class SprintStatePatternTests
    {
        // Helper to capture console output
        private string CaptureConsoleOutput(Action action)
        {
            var sw = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(sw);
            try
            {
                action();
                return sw.ToString();
            }
            finally
            {
                Console.SetOut(originalOut);
                sw.Dispose();
            }
        }

        // Helper to access private _state field
        private ISprintState GetPrivateState(Sprint sprint)
        {
            var field = typeof(Sprint).GetField("_state", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return (ISprintState)field.GetValue(sprint);
        }

        // -----------------------------
        // CreatedState Tests
        // -----------------------------
        [Fact]
        public void CreatedState_StartSprint_ShouldTransitionToInProgress()
        {
            var sprint = new ReviewSprint();
            var output = CaptureConsoleOutput(() => sprint.StartSprint());
            Assert.Equal(sprint.InProgressState, GetPrivateState(sprint));
            Assert.Contains("Sprint was started", output);
        }

        [Fact]
        public void CreatedState_OtherMethods_ShouldThrow()
        {
            var sprint = new ReviewSprint();
            Assert.Throws<InvalidOperationException>(() => sprint.FinishSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.FinalizeSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.RetryRelease());
            Assert.Throws<InvalidOperationException>(() => sprint.CloseSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.CancelSprint());
        }

        [Fact]
        public void CreatedState_CanAddBacklogItem_AndUpdateDetails()
        {
            var sprint = new ReviewSprint();
            var backlogItem = new BacklogItem();
            sprint.AddBacklogItem(backlogItem);
            Assert.Contains(backlogItem, sprint.BacklogItems);

            sprint.UpdateDetails("Sprint1", DateTime.Today, DateTime.Today.AddDays(5));
            Assert.Equal("Sprint1", sprint.Name);
        }

        // -----------------------------
        // InProgressState Tests
        // -----------------------------
        [Fact]
        public void InProgressState_FinishSprint_ShouldTransitionToFinished()
        {
            var sprint = new ReviewSprint();
            sprint.setState(sprint.InProgressState);
            var output = CaptureConsoleOutput(() => sprint.FinishSprint());
            Assert.Equal(sprint.FinishedState, GetPrivateState(sprint));
            Assert.Contains("Sprint is being finished", output);
        }

        [Fact]
        public void InProgressState_OtherMethods_ShouldThrow()
        {
            var sprint = new ReviewSprint();
            sprint.setState(sprint.InProgressState);
            Assert.Throws<InvalidOperationException>(() => sprint.StartSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.FinalizeSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.RetryRelease());
            Assert.Throws<InvalidOperationException>(() => sprint.CloseSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.CancelSprint());
        }

        // -----------------------------
        // FinishedState Tests
        // -----------------------------
        [Fact]
        public void FinishedState_FinalizeSprint_ShouldTransitionToFinalized()
        {
            var sprint = new ReviewSprint();
            sprint.setState(sprint.FinishedState);
            var output = CaptureConsoleOutput(() => sprint.FinalizeSprint());
            Assert.Equal(sprint.FinalizedState, GetPrivateState(sprint));
            Assert.Contains("Finalizing sprint", output);
        }

        [Fact]
        public void FinishedState_CancelSprint_ShouldTransitionToCancelled()
        {
            var sprint = new ReviewSprint();
            sprint.setState(sprint.FinishedState);
            var output = CaptureConsoleOutput(() => sprint.CancelSprint());
            Assert.Equal(sprint.CancelledState, GetPrivateState(sprint));
            Assert.Contains("Sprint is cancelled", output);
        }

        [Fact]
        public void FinishedState_OtherMethods_ShouldThrow()
        {
            var sprint = new ReviewSprint();
            sprint.setState(sprint.FinishedState);
            Assert.Throws<InvalidOperationException>(() => sprint.StartSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.FinishSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.RetryRelease());
            Assert.Throws<InvalidOperationException>(() => sprint.CloseSprint());
        }

        // -----------------------------
        // FinalizedState Tests (ReviewSprint)
        // -----------------------------
        [Fact]
        public void FinalizedState_ReviewSprint_CloseSprint_ShouldTransitionToClosed()
        {
            var sprint = new ReviewSprint();
            sprint.setState(sprint.FinalizedState);
            var output = CaptureConsoleOutput(() => sprint.CloseSprint());
            Assert.Equal(sprint.ClosedState, GetPrivateState(sprint));
            Assert.Contains("Closing sprint with a review", output);
        }

        [Fact]
        public void FinalizedState_ReviewSprint_OtherMethods_ShouldThrow()
        {
            var sprint = new ReviewSprint();
            sprint.setState(sprint.FinalizedState);
            Assert.Throws<InvalidOperationException>(() => sprint.StartSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.FinishSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.FinalizeSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.RetryRelease());
            Assert.Throws<InvalidOperationException>(() => sprint.CancelSprint());
        }

        // -----------------------------
        // FinalizedState Tests (DeploymentSprint)
        // -----------------------------
        [Fact]
        public void FinalizedState_DeploymentSprint_CloseSprint_ShouldTransitionToFinished()
        {
            var sprint = new DeploymentSprint();
            sprint.setState(sprint.FinalizedState);
            var output = CaptureConsoleOutput(() => sprint.CloseSprint());
            Assert.Equal(sprint.FinishedState, GetPrivateState(sprint));
            Assert.Contains("Closing sprint with a deployment", output);
        }

        [Fact]
        public void FinalizedState_DeploymentSprint_RetrySprint_ShouldTransitionToFinished()
        {
            var sprint = new DeploymentSprint();
            sprint.setState(sprint.FinalizedState);
            var output = CaptureConsoleOutput(() => sprint.RetryRelease());
            Assert.Equal(sprint.FinishedState, GetPrivateState(sprint));
            Assert.Contains("Retrying release again", output);
        }

        [Fact]
        public void FinalizedState_DeploymentSprint_OtherMethods_ShouldThrow()
        {
            var sprint = new DeploymentSprint();
            sprint.setState(sprint.FinalizedState);
            Assert.Throws<InvalidOperationException>(() => sprint.StartSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.FinishSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.FinalizeSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.CancelSprint());
        }

        // -----------------------------
        // CancelledState Tests
        // -----------------------------
        [Fact]
        public void CancelledState_CloseSprint_ShouldTransitionToClosed()
        {
            var sprint = new ReviewSprint();
            sprint.setState(sprint.CancelledState);
            var output = CaptureConsoleOutput(() => sprint.CloseSprint());
            Assert.Equal(sprint.ClosedState, GetPrivateState(sprint));
            Assert.Contains("Closing cancelled state", output);
        }

        [Fact]
        public void CancelledState_OtherMethods_ShouldThrow()
        {
            var sprint = new ReviewSprint();
            sprint.setState(sprint.CancelledState);
            Assert.Throws<InvalidOperationException>(() => sprint.StartSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.FinishSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.FinalizeSprint());
            Assert.Throws<InvalidOperationException>(() => sprint.RetryRelease());
            Assert.Throws<InvalidOperationException>(() => sprint.CancelSprint());
        }

        // -----------------------------
        // Test blocking updates when not in CreatedState
        // -----------------------------
        [Fact]
        public void CannotUpdateDetails_WhenNotInCreatedState()
        {
            var sprint = new ReviewSprint();
            sprint.setState(sprint.InProgressState);
            var output = CaptureConsoleOutput(() =>
                sprint.UpdateDetails("Test", DateTime.Now, DateTime.Now.AddDays(1)));
            Assert.Contains("Not allowed to change the details of the sprint", output);
        }

        [Fact]
        public void CannotAddBacklogItem_WhenNotInCreatedState()
        {
            var sprint = new ReviewSprint();
            sprint.setState(sprint.InProgressState);
            var backlogItem = new BacklogItem();
            var output = CaptureConsoleOutput(() => sprint.AddBacklogItem(backlogItem));
            Assert.Contains("Not allowed to add an item to the backlog", output);
        }
    }
}
using Xunit;
using SOA3.Domain.BacklogPatterns;
using SOA3.Domain.SprintPatterns;
using SOA3.Domain.SprintPatterns.SprintStatePattern;
using System;

namespace SOA3.Tests.SprintTests
{
    public class SprintTests
    {

        /// Helper method to access the private _state field of a Sprint.
        /// Used to verify if the sprint state has changed correctly after actions.
        private ISprintState GetPrivateState(Sprint sprint)
        {
            var field = typeof(Sprint).GetField("_state", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return (ISprintState)field.GetValue(sprint);
        }



        /// Verify that a backlog item can be added when the sprint is in the Created state.
        /// Corresponds to functional requirement FR-1 / FR-2.
        /// 1
        [Fact]
        public void ReviewSprint_CanAddBacklogItem_InCreatedState()
        {
            var sprint = new ReviewSprint();
            var item = new BacklogItem("Item1");

            sprint.AddBacklogItem(item);

            Assert.Contains(item, sprint.BacklogItems);
        }

        /// Verify that a backlog item cannot be added when the sprint is in the Finished state.
        /// Tests edge case where business rules prevent modifications.
        /// 2
        [Fact]
        public void ReviewSprint_CannotAddBacklogItem_InFinishedState()
        {
            var sprint = new ReviewSprint();
            var item = new BacklogItem("Item1");

            sprint.setState(sprint.FinishedState);
            sprint.AddBacklogItem(item);

            Assert.DoesNotContain(item, sprint.BacklogItems);
        }


        /// Verify that sprint details (name, start date, end date) can be updated when the sprint is in the Created state.
        /// 3
        [Fact]
        public void ReviewSprint_CanUpdateDetails_InCreatedState()
        {
            var sprint = new ReviewSprint();
            var start = DateTime.Today;
            var end = DateTime.Today.AddDays(7);

            sprint.UpdateDetails("Sprint1", start, end);

            Assert.Equal("Sprint1", sprint.Name);
            Assert.Equal(start, sprint.StartDateTime);
            Assert.Equal(end, sprint.EndDateTime);
        }

        /// Verify that sprint details cannot be updated when the sprint is in the Finished state.
        /// Ensures business rules prevent illegal modifications.
        /// 4
        [Fact]
        public void ReviewSprint_CannotUpdateDetails_InFinishedState()
        {
            var sprint = new ReviewSprint();
            var start = DateTime.Today;
            var end = DateTime.Today.AddDays(7);

            sprint.setState(sprint.FinishedState);
            sprint.UpdateDetails("Sprint1", start, end);

            Assert.NotEqual("Sprint1", sprint.Name);
        }



        /// Verify that a sprint can be started when it is in the Created state.
        /// Confirms correct state transition to InProgressState.
        /// 5
        [Fact]
        public void Sprint_CanBeStarted_FromCreatedState()
        {
            var sprint = new ReviewSprint();
            sprint.StartSprint();

            Assert.Equal(sprint.InProgressState, GetPrivateState(sprint));
        }

        /// Verify that starting a sprint from the Finished state is not allowed.
        /// Tests enforcement of sprint lifecycle rules.
        /// 6
        [Fact]
        public void Sprint_CannotBeStarted_FromFinishedState()
        {
            var sprint = new ReviewSprint();
            sprint.setState(sprint.FinishedState);

            Assert.Throws<InvalidOperationException>(() => sprint.StartSprint());
        }

        /// Verify that a sprint can be finished when it is in the InProgress state.
        /// Ensures correct state transition to FinishedState.
        /// 7
        [Fact]
        public void Sprint_CanBeFinished_FromInProgressState()
        {
            var sprint = new ReviewSprint();
            sprint.StartSprint();

            sprint.FinishSprint();

            Assert.Equal(sprint.FinishedState, GetPrivateState(sprint));
        }

        /// Verify that finishing a sprint from the Created state is not allowed.
        /// Ensures enforcement of sprint lifecycle rules.
        /// 8
        [Fact]
        public void Sprint_CannotBeFinished_FromCreatedState()
        {
            var sprint = new ReviewSprint();
            Assert.Throws<InvalidOperationException>(() => sprint.FinishSprint());
        }
    }
}
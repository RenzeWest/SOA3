using Moq;
using SOA3.Domain.BacklogPatterns;
using System;
using Xunit;

namespace SOA3.Tests.BacklogItemTests
{
    public class BacklogItemTests
    {
        // 1. Initial state
        [Fact]
        public void BacklogItem_Should_Start_In_Todo_State()
        {
            // Verify that a new backlog item starts in the TODO state
            var item = new BacklogItem();
            var state = item.GetState();
            Assert.IsType<TodoState>(state);
        }

        // 2. Valid transition: TODO → DOING
        [Fact]
        public void Should_Move_From_Todo_To_Doing()
        {
            // Verify valid state transition from TODO to DOING
            var item = new BacklogItem();
            item.StartWork();
            Assert.IsType<DoingState>(item.GetState());
        }

        // 3. Invalid transition (skip states)
        [Fact]
        public void Should_Not_Allow_ApproveTest_From_Todo()
        {
            // Ensure approving test from TODO state throws exception
            var item = new BacklogItem();
            Assert.Throws<InvalidOperationException>(() => item.ApproveTest());
        }

        // 4. Full happy flow to TESTED
        [Fact]
        public void Should_Follow_Happy_Flow_Until_Tested()
        {
            // Verify full lifecycle until TESTED state
            var item = new BacklogItem();
            item.StartWork();              // TODO → DOING
            item.MarkReadyForTesting();    // DOING → READY FOR TESTING
            item.StartTesting();           // → TESTING
            item.ApproveTest();            // → TESTED

            Assert.IsType<TestedState>(item.GetState());
        }

        // 5. Reject → terug naar TODO
        [Fact]
        public void Should_Return_To_Todo_When_Rejected()
        {
            // Verify reject to TODO works from READY FOR TESTING
            var item = new BacklogItem();
            item.StartWork();
            item.MarkReadyForTesting();

            item.RejectToDo();
            Assert.IsType<TodoState>(item.GetState());
        }

        // 6. AcceptDone without all activities completed → should fail
        [Fact]
        public void Should_Not_Allow_Done_When_Activities_Not_Completed()
        {
            // Ensure DONE is not allowed if activities are not completed
            var item = new BacklogItem();

            var activityMock = new Mock<Activity>();
            activityMock.Setup(a => a.GetState())
                        .Returns(new TodoState(activityMock.Object)); // not done
            item.AddActivity(activityMock.Object);

            item.StartWork();
            item.MarkReadyForTesting();
            item.StartTesting();
            item.ApproveTest();

            Assert.Throws<InvalidOperationException>(() => item.AcceptDone());
            Assert.IsNotType<DoneState>(item.GetState());
        }

        // 6b. Done blocked if any activity not completed
        [Fact]
        public void Should_Not_Allow_Done_If_Any_Activity_Not_Completed()
        {
            // Verify DONE is blocked if at least one activity is not done
            var item = new BacklogItem();

            var activityDone = new Mock<Activity>();
            activityDone.Setup(a => a.GetState()).Returns(item.DoneState);

            var activityTodo = new Mock<Activity>();
            activityTodo.Setup(a => a.GetState()).Returns(new TodoState(activityTodo.Object));

            item.AddActivity(activityDone.Object);
            item.AddActivity(activityTodo.Object);

            item.StartWork();
            item.MarkReadyForTesting();
            item.StartTesting();
            item.ApproveTest();

            Assert.Throws<InvalidOperationException>(() => item.AcceptDone());
        }

        // 7. AcceptDone with all activities done → should succeed
        [Fact]
        public void Should_Allow_Done_When_All_Activities_Are_Completed()
        {
            // Verify DONE is allowed when all activities are completed
            var item = new BacklogItem();
            var doneState = item.DoneState;

            var activityMock = new Mock<Activity>();
            activityMock.Setup(a => a.GetState()).Returns(doneState);

            item.AddActivity(activityMock.Object);

            item.StartWork();
            item.MarkReadyForTesting();
            item.StartTesting();
            item.ApproveTest();
            item.AcceptDone();

            Assert.IsType<DoneState>(item.GetState());
        }

        // 8. Reject from TESTING → back to READY FOR TESTING
        [Fact]
        public void Should_Return_To_ReadyForTesting_When_Rejected_From_Testing()
        {
            // Verify rejecting from TESTING moves item back to READY FOR TESTING
            var item = new BacklogItem();
            item.StartWork();
            item.MarkReadyForTesting();
            item.StartTesting();

            item.RejectToReadyForTesting();
            Assert.IsType<ReadyForTestingState>(item.GetState());
        }

        // 9. Edge case: StartTesting without being READY FOR TESTING
        [Fact]
        public void Should_Not_Allow_StartTesting_If_Not_ReadyForTesting()
        {
            // Ensure starting TESTING before READY FOR TESTING throws exception
            var item = new BacklogItem();
            item.StartWork(); // TODO → DOING

            Assert.Throws<InvalidOperationException>(() => item.StartTesting());
        }
    }
}
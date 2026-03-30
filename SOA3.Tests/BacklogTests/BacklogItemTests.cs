using Moq;
using SOA3.Domain.BacklogPatterns;
using System;
using Xunit;

namespace SOA3.Tests.BacklogPatterns
{
    // ----------------------------------------
    // Helper for testing invalid operations
    // ----------------------------------------
    public static class StateTestHelper
    {
        public static void AssertInvalidOperations(IBacklogItemState state, string[] allowedMethods)
        {
            if (!allowedMethods.Contains("StartWork"))
                Assert.Throws<InvalidOperationException>(() => state.StartWork());

            if (!allowedMethods.Contains("MarkReadyForTesting"))
                Assert.Throws<InvalidOperationException>(() => state.MarkReadyForTesting());

            if (!allowedMethods.Contains("StartTesting"))
                Assert.Throws<InvalidOperationException>(() => state.StartTesting());

            if (!allowedMethods.Contains("ApproveTest"))
                Assert.Throws<InvalidOperationException>(() => state.ApproveTest());

            if (!allowedMethods.Contains("AcceptDone"))
                Assert.Throws<InvalidOperationException>(() => state.AcceptDone());

            if (!allowedMethods.Contains("RejectToDo"))
                Assert.Throws<InvalidOperationException>(() => state.RejectToDo());

            if (!allowedMethods.Contains("RejectToReadyForTesting"))
                Assert.Throws<InvalidOperationException>(() => state.RejectToReadyForTesting());
        }
    }

    // ----------------------------------------
    // TODO State Tests
    // ----------------------------------------
    public class TodoStateTests
    {
        [Fact]
        public void StartWork_ShouldTransitionToDoing()
        {
            var activity = new Activity();
            var state = new TodoState(activity);

            state.StartWork();

            Assert.Equal(activity.DoingState, activity.GetState());
        }

        [Fact]
        public void OtherMethods_ShouldThrowInvalidOperationException()
        {
            var activity = new Activity();
            var state = new TodoState(activity);

            StateTestHelper.AssertInvalidOperations(state, allowedMethods: new[] { "StartWork" });
        }
    }

    // ----------------------------------------
    // ReadyForTesting State Tests
    // ----------------------------------------
    public class ReadyForTestingStateTests
    {
        [Fact]
        public void StartTesting_ShouldTransitionToTesting()
        {
            var activity = new Activity();
            var state = new ReadyForTestingState(activity);

            state.StartTesting();

            Assert.Equal(activity.TestingState, activity.GetState());
        }

        [Fact]
        public void RejectToDo_ShouldTransitionToTodo()
        {
            var activity = new Activity();
            var state = new ReadyForTestingState(activity);

            state.RejectToDo();

            Assert.Equal(activity.TodoState, activity.GetState());
        }

        [Fact]
        public void RejectToReadyForTesting_ShouldStayInReadyForTesting()
        {
            var activity = new Activity();
            var state = new ReadyForTestingState(activity);

            state.RejectToReadyForTesting();

            Assert.Equal(activity.ReadyForTestingState, activity.GetState());
        }

        [Fact]
        public void OtherMethods_ShouldThrowInvalidOperationException()
        {
            var activity = new Activity();
            var state = new ReadyForTestingState(activity);

            StateTestHelper.AssertInvalidOperations(state,
                allowedMethods: new[] { "StartTesting", "RejectToDo", "RejectToReadyForTesting" });
        }
    }

    // ----------------------------------------
    // Testing State Tests
    // ----------------------------------------
    public class TestingStateTests
    {
        [Fact]
        public void ApproveTest_ShouldTransitionToTested()
        {
            var activity = new Activity();
            var state = new TestingState(activity);

            state.ApproveTest();

            Assert.Equal(activity.TestedState, activity.GetState());
        }

        [Fact]
        public void RejectToDo_ShouldTransitionToTodo()
        {
            var activity = new Activity();
            var state = new TestingState(activity);

            state.RejectToDo();

            Assert.Equal(activity.TodoState, activity.GetState());
        }

        [Fact]
        public void RejectToReadyForTesting_ShouldTransitionToReadyForTesting()
        {
            var activity = new Activity();
            var state = new TestingState(activity);

            state.RejectToReadyForTesting();

            Assert.Equal(activity.ReadyForTestingState, activity.GetState());
        }

        [Fact]
        public void OtherMethods_ShouldThrowInvalidOperationException()
        {
            var activity = new Activity();
            var state = new TestingState(activity);

            StateTestHelper.AssertInvalidOperations(state,
                allowedMethods: new[] { "ApproveTest", "RejectToDo", "RejectToReadyForTesting" });
        }
    }

    // ----------------------------------------
    // Tested State Tests
    // ----------------------------------------
    public class TestedStateTests
    {
        [Fact]
        public void AcceptDone_ShouldTransitionToDone()
        {
            var activity = new Activity();
            var state = new TestedState(activity);

            state.AcceptDone();

            Assert.Equal(activity.DoneState, activity.GetState());
        }

        [Fact]
        public void RejectToDo_ShouldTransitionToTodo()
        {
            var activity = new Activity();
            var state = new TestedState(activity);

            state.RejectToDo();

            Assert.Equal(activity.TodoState, activity.GetState());
        }

        [Fact]
        public void RejectToReadyForTesting_ShouldTransitionToReadyForTesting()
        {
            var activity = new Activity();
            var state = new TestedState(activity);

            state.RejectToReadyForTesting();

            Assert.Equal(activity.ReadyForTestingState, activity.GetState());
        }

        [Fact]
        public void OtherMethods_ShouldThrowInvalidOperationException()
        {
            var activity = new Activity();
            var state = new TestedState(activity);

            StateTestHelper.AssertInvalidOperations(state,
                allowedMethods: new[] { "AcceptDone", "RejectToDo", "RejectToReadyForTesting" });
        }
    }

    // ----------------------------------------
    // Done State Tests
    // ----------------------------------------
    public class DoneStateTests
    {
        [Fact]
        public void AllMethods_ShouldThrowNotImplementedException()
        {
            var activity = new Activity();
            var state = new DoneState(activity);

            Assert.Throws<NotImplementedException>(() => state.StartWork());
            Assert.Throws<NotImplementedException>(() => state.MarkReadyForTesting());
            Assert.Throws<NotImplementedException>(() => state.StartTesting());
            Assert.Throws<NotImplementedException>(() => state.ApproveTest());
            Assert.Throws<NotImplementedException>(() => state.AcceptDone());
            Assert.Throws<NotImplementedException>(() => state.RejectToDo());
            Assert.Throws<NotImplementedException>(() => state.RejectToReadyForTesting());
        }
    }
}
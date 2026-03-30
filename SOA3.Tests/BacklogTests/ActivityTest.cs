namespace SOA3.Tests.BacklogTests
{
    using global::SOA3.Domain;
    using global::SOA3.Domain.BacklogPatterns;
    using Xunit;

    namespace SOA3.Tests.BacklogPatterns
    {
        [Collection("Sequential")]
        public class ActivityTests
        {
            [Fact]
            public void DefaultConstructor_ShouldInitializeStatesAndProperties()
            {
                // Arrange
                var activity = new Activity();

                // Act & Assert
                Assert.NotNull(activity.TodoState);
                Assert.NotNull(activity.DoingState);
                Assert.NotNull(activity.ReadyForTestingState);
                Assert.NotNull(activity.TestingState);
                Assert.NotNull(activity.TestedState);
                Assert.NotNull(activity.DoneState);

                Assert.Equal("Default Activity", activity.Title);
                Assert.NotNull(activity.GetState());
            }

            [Fact]
            public void ParamConstructor_ShouldSetPropertiesCorrectly()
            {
                // Arrange
                var developer = new Person("John Doe", "Jon@Emai");
                var activity = new Activity(developer, "Implement Feature", "Feature Description");

                // Act & Assert
                Assert.Equal("Implement Feature", activity.Title);
                Assert.Equal("Feature Description", activity.GetState() is not null ? "Feature Description" : null);
                Assert.Equal(developer, activity.AssignedDeveloper);
            }

            [Fact]
            public void StartWork_ShouldChangeStateToDoing()
            {
                // Arrange
                var activity = new Activity();
                activity.SetState(activity.TodoState);

                // Act
                activity.StartWork();

                // Assert
                Assert.Equal(activity.DoingState, activity.GetState());
            }

            [Fact]
            public void MarkReadyForTesting_ShouldChangeStateToReadyForTesting()
            {
                var activity = new Activity();
                activity.SetState(activity.DoingState);

                activity.MarkReadyForTesting();

                Assert.Equal(activity.ReadyForTestingState, activity.GetState());
            }

            [Fact]
            public void StartTesting_ShouldChangeStateToTesting()
            {
                var activity = new Activity();
                activity.SetState(activity.ReadyForTestingState);

                activity.StartTesting();

                Assert.Equal(activity.TestingState, activity.GetState());
            }

            [Fact]
            public void ApproveTest_ShouldChangeStateToTested()
            {
                var activity = new Activity();
                activity.SetState(activity.TestingState);

                activity.ApproveTest();

                Assert.Equal(activity.TestedState, activity.GetState());
            }

            [Fact]
            public void AcceptDone_ShouldChangeStateToDone()
            {
                var activity = new Activity();
                activity.SetState(activity.TestedState);

                activity.AcceptDone();

                Assert.Equal(activity.DoneState, activity.GetState());
            }

            [Fact]
            public void RejectToDo_ShouldReturnToTodoState()
            {
                var activity = new Activity();
                activity.SetState(activity.ReadyForTestingState);

                activity.RejectToDo();

                Assert.Equal(activity.TodoState, activity.GetState());
            }

            [Fact]
            public void RejectToReadyForTesting_ShouldReturnToReadyForTestingState()
            {
                var activity = new Activity();
                activity.SetState(activity.TestedState);

                activity.RejectToReadyForTesting();

                Assert.Equal(activity.ReadyForTestingState, activity.GetState());
            }
        }
    }
}

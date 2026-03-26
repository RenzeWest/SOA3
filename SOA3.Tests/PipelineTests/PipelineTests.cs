using SOA3.Domain.PipelinePatterns;
using SOA3.Domain.PipelinePatterns.CompositePattern;
using SOA3.Domain.PipelinePatterns.CompositePattern.Leafs;
using SOA3.Domain.PipelinePatterns.VisitorPattern;
using System;
using System.Collections.Generic;
using Xunit;

namespace SOA3.Tests.PipelineTests
{
    public class PipelineTests
    {
        private TestPipelineAction CreateTestAction() => new TestPipelineAction();

        [Fact]
        public void PipelineComposite_Should_AddAndRemoveActions()
        {
            var composite = new PipelineCompositeImpl();
            var action1 = CreateTestAction();
            var action2 = CreateTestAction();

            composite.Add(action1);
            composite.Add(action2);

            Assert.Equal(action1, composite.GetComponent(0));
            Assert.Equal(action2, composite.GetComponent(1));

            composite.Remove(action1);
            Assert.Equal(action2, composite.GetComponent(0));
        }

        [Fact]
        public void PipelineComposite_Should_AcceptVisitorForAllActions()
        {
            var composite = new PipelineCompositeImpl();
            var action1 = CreateTestAction();
            var action2 = CreateTestAction();

            composite.Add(action1);
            composite.Add(action2);

            var visitor = new MockPipelineVisitor();
            composite.AcceptVisitors(visitor);

            Assert.Equal(2, visitor.VisitedActions.Count);
            Assert.Contains(action1, visitor.VisitedActions);
            Assert.Contains(action2, visitor.VisitedActions);
        }

        [Fact]
        public void DevelopmentPipeline_Should_AddAndRemoveActions()
        {
            var pipeline = new DevelopmentPipeline();
            var action = CreateTestAction();

            pipeline.AddPipelineAction(action);

            var actions = GetPipelineActions(pipeline);
            Assert.Single(actions);
            Assert.Contains(action, actions);

            pipeline.RemovePipelineAction(action);
            Assert.Empty(actions);
        }

        [Fact]
        public void Visitor_RunVisitor_Should_HandleTestAction()
        {
            var action = CreateTestAction();
            var visitor = new MockPipelineVisitor();

            action.AcceptVisitor(visitor);

            Assert.Contains(action, visitor.VisitedActions);
        }

        // --------------------------------------------
        // Extra tests: simulate pipeline execution with sprint
        // --------------------------------------------

        private class TestSprint
        {
            public string Status { get; set; } = "Finished";
            public bool NotificationSent { get; set; } = false;
            public void Notify() => NotificationSent = true;
        }

        [Fact]
        public void Pipeline_Should_ReleaseSprint_WhenAllActionsSuccessful()
        {
            // Arrange
            var pipeline = new DevelopmentPipeline();
            pipeline.AddPipelineAction(new AnalysePipelineAction());
            pipeline.AddPipelineAction(new BuildPipelineAction());
            pipeline.AddPipelineAction(new DeployPipelineAction());

            var sprint = new TestSprint();
            var visitor = new RunVisitorMock(sprint);

            // Act
            foreach (var action in GetPipelineActions(pipeline))
                action.AcceptVisitor(visitor);

            // Assert
            Assert.Equal("Released", sprint.Status);
            Assert.True(sprint.NotificationSent);
        }

        [Fact]
        public void Pipeline_Should_NotReleaseSprint_WhenActionFails()
        {
            // Arrange
            var pipeline = new DevelopmentPipeline();
            pipeline.AddPipelineAction(new AnalysePipelineAction());
            pipeline.AddPipelineAction(new FailPipelineAction());
            pipeline.AddPipelineAction(new BuildPipelineAction());

            var sprint = new TestSprint();
            var visitor = new RunVisitorMock(sprint);

            // Act & Assert
            Assert.Throws<Exception>(() =>
            {
                foreach (var action in GetPipelineActions(pipeline))
                    action.AcceptVisitor(visitor);
            });

            Assert.Equal("Finished", sprint.Status);
            Assert.False(sprint.NotificationSent);
        }

        [Fact]
        public void Pipeline_Should_BeRestartable_AfterFailure()
        {
            // Arrange
            var pipeline = new DevelopmentPipeline();
            pipeline.AddPipelineAction(new FailPipelineAction());
            pipeline.AddPipelineAction(new DeployPipelineAction());

            var sprint = new TestSprint();
            var visitor = new RunVisitorMock(sprint);

            // First execution fails
            Assert.Throws<Exception>(() =>
            {
                foreach (var action in GetPipelineActions(pipeline))
                    action.AcceptVisitor(visitor);
            });

            // Correct the failure: replace FailPipelineAction with BuildPipelineAction
            var actions = GetPipelineActions(pipeline);
            actions[0] = new BuildPipelineAction();

            // Re-run pipeline
            foreach (var action in GetPipelineActions(pipeline))
                action.AcceptVisitor(visitor);

            // Assert sprint is now released
            Assert.Equal("Released", sprint.Status);
            Assert.True(sprint.NotificationSent);
        }

        // --------------------------------------------
        // Helpers and mocks
        // --------------------------------------------

        private List<PipelineActionComponent> GetPipelineActions(DevelopmentPipeline pipeline)
        {
            return typeof(DevelopmentPipeline)
                .GetField("_actions", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .GetValue(pipeline) as List<PipelineActionComponent>;
        }

        private class MockPipelineVisitor : IPipelineVisitor
        {
            public List<PipelineActionComponent> VisitedActions { get; } = new();
            public void VisitAnalysePipelineAction(AnalysePipelineAction action) => VisitedActions.Add(action);
            public void VisitBuildPipelineAction(BuildPipelineAction action) => VisitedActions.Add(action);
            public void VisitDeployPipelineAction(DeployPipelineAction action) => VisitedActions.Add(action);
            public void VisitPackagePipelineAction(PackagePipelineAction action) => VisitedActions.Add(action);
            public void VisitSourcePipelineAction(SourcePipelineAction action) => VisitedActions.Add(action);
            public void VisitTestPipelineAction(TestPipelineAction action) => VisitedActions.Add(action);
            public void VisitUtilityPipelineAction(UtilityPipelineAction action) => VisitedActions.Add(action);
            public void PipelineFailed(Exception exception) => throw new Exception("Pipeline failed");
        }

        private class PipelineCompositeImpl : PipelineComposite
        {
            public override void AcceptVisitor(IPipelineVisitor visitor) => AcceptVisitors(visitor);
        }

        private class FailPipelineAction : PipelineActionComponent
        {
            public override void AcceptVisitor(IPipelineVisitor visitor) =>
                visitor.PipelineFailed(new Exception("Pipeline failed"));
        }

        private class RunVisitorMock : IPipelineVisitor
        {
            private readonly TestSprint _sprint;
            public RunVisitorMock(TestSprint sprint) => _sprint = sprint;

            public void PipelineFailed(Exception exception) { throw exception; }
            public void VisitAnalysePipelineAction(AnalysePipelineAction action) { }
            public void VisitBuildPipelineAction(BuildPipelineAction action) { }
            public void VisitDeployPipelineAction(DeployPipelineAction action)
            {
                _sprint.Status = "Released";
                _sprint.Notify();
            }
            public void VisitPackagePipelineAction(PackagePipelineAction action) { }
            public void VisitSourcePipelineAction(SourcePipelineAction action) { }
            public void VisitTestPipelineAction(TestPipelineAction action) { }
            public void VisitUtilityPipelineAction(UtilityPipelineAction action) { }
        }
    }
}
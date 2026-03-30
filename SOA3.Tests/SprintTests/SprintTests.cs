using SOA3.Domain.BacklogPatterns;
using SOA3.Domain.SprintPatterns;
using SOA3.Domain.SprintPatterns.ReportPatterns;
using SOA3.Domain.SprintPatterns.SprintStatePattern;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace SOA3.Tests.SprintTests
{
    public class ReviewSprintStateTests
    {
        private ISprintState GetPrivateState(Sprint sprint)
        {
            var field = typeof(Sprint)
                .GetField("_state", BindingFlags.NonPublic | BindingFlags.Instance);

            return (ISprintState)field.GetValue(sprint);
        }

        // -------------------------
        // Initial State
        // -------------------------
        [Fact]
        public void Sprint_DefaultState_ShouldBeCreatedState()
        {
            var sprint = new ReviewSprint();

            Assert.Equal(sprint.CreatedState, GetPrivateState(sprint));
        }

        // -------------------------
        // Created State
        // -------------------------
        [Fact]
        public void CreatedState_StartSprint_ShouldTransitionToInProgress()
        {
            var sprint = new ReviewSprint();

            sprint.StartSprint();

            Assert.Equal(sprint.InProgressState, GetPrivateState(sprint));
        }

        [Fact]
        public void CreatedState_FinishSprint_ShouldThrow()
        {
            var sprint = new ReviewSprint();

            Assert.Throws<InvalidOperationException>(() => sprint.FinishSprint());
        }

        // -------------------------
        // In Progress State
        // -------------------------
        [Fact]
        public void InProgressState_FinishSprint_ShouldTransitionToFinished()
        {
            var sprint = new ReviewSprint();
            sprint.setState(sprint.InProgressState);

            sprint.FinishSprint();

            Assert.Equal(sprint.FinishedState, GetPrivateState(sprint));
        }

        // -------------------------
        // Finished State
        // -------------------------
        [Fact]
        public void FinishedState_StartSprint_ShouldThrow()
        {
            var sprint = new ReviewSprint();
            sprint.setState(sprint.FinishedState);

            Assert.Throws<InvalidOperationException>(() => sprint.StartSprint());
        }

        // -------------------------
        // Finalized State (Reviewing)
        // -------------------------
        [Fact]
        public void ReviewingState_CannotStartSprint_ShouldThrow()
        {
            var sprint = new ReviewSprint();
            sprint.setState(sprint.FinalizedState);

            Assert.Throws<InvalidOperationException>(() => sprint.StartSprint());
        }

        [Fact]
        public void ReviewingState_CannotFinishSprint_ShouldThrow()
        {
            var sprint = new ReviewSprint();
            sprint.setState(sprint.FinalizedState);

            Assert.Throws<InvalidOperationException>(() => sprint.FinishSprint());
        }

        // -------------------------
        // Backlog
        // -------------------------
        [Fact]
        public void AddBacklogItem_ShouldNotAdd_WhenNotInCreatedState()
        {
            var sprint = new ReviewSprint();
            var item = new BacklogItem("Item1");

            sprint.setState(sprint.InProgressState);
            sprint.AddBacklogItem(item);

            Assert.DoesNotContain(item, sprint.BacklogItems);
        }

        [Fact]
        public void AddNullBacklogItem_ShouldThrowArgumentNullException()
        {
            var sprint = new ReviewSprint();

            Assert.Throws<ArgumentNullException>(() => sprint.AddBacklogItem(null));
        }

        // -------------------------
        // Update Details
        // -------------------------
        [Fact]
        public void UpdateDetails_InCreatedState_ShouldChangeDetails()
        {
            var sprint = new ReviewSprint();
            var start = DateTime.Today;
            var end = DateTime.Today.AddDays(5);

            sprint.UpdateDetails("SprintA", start, end);

            Assert.Equal("SprintA", sprint.Name);
            Assert.Equal(start, sprint.StartDateTime);
            Assert.Equal(end, sprint.EndDateTime);
        }

        [Fact]
        public void UpdateDetails_InFinishedState_ShouldNotChangeDetails()
        {
            var sprint = new ReviewSprint();
            sprint.setState(sprint.FinishedState);

            var start = DateTime.Today;
            var end = DateTime.Today.AddDays(7);

            sprint.UpdateDetails("Name", start, end);

            Assert.NotEqual("Name", sprint.Name);
        }


    }

    public class ExportFormatTests
    {
        [Fact]
        public void ExportFormat_ShouldHaveCorrectValues()
        {
            Assert.Equal(0, (int)ExportFormat.PDF);
            Assert.Equal(1, (int)ExportFormat.PNG);
        }
    }

    public class DocumentTests
    {
        [Fact]
        public void Document_ShouldInitializeGuid()
        {
            var doc = new Document();

            var field = typeof(Document)
                .GetField("_id", BindingFlags.NonPublic | BindingFlags.Instance);

            var id = (System.Guid)field.GetValue(doc);

            Assert.NotEqual(System.Guid.Empty, id);
        }
    }

    public class ReportSettingsTests
    {
        [Fact]
        public void ReportSettings_DefaultFileFormats_ShouldBeEmpty()
        {
            var settings = new ReportSettings();

            var field = typeof(ReportSettings)
                .GetField("_fileFormats", BindingFlags.NonPublic | BindingFlags.Instance);

            var formats = (List<ExportFormat>)field.GetValue(settings);

            Assert.NotNull(formats);
            Assert.Empty(formats);
        }
    }

    public class SprintImplementationTests
    {
        [Fact]
        public void ReviewSprint_ShouldHaveReviewingState()
        {
            var sprint = new ReviewSprint();

            Assert.NotNull(sprint.FinalizedState);
            Assert.IsType<ReviewingState>(sprint.FinalizedState);
        }

        [Fact]
        public void DeploymentSprint_ShouldHaveReleasingState()
        {
            var sprint = new DeploymentSprint();

            Assert.NotNull(sprint.FinalizedState);
            Assert.IsType<ReleasingState>(sprint.FinalizedState);
        }
    }

    public class ExportReportStrategyTests
    {
        [Fact]
        public void PDFExportReport_ShouldThrowNotImplemented()
        {
            var strategy = new PDFExportReport();
            var settings = new ReportSettings();

            Assert.Throws<NotImplementedException>(() => strategy.ExportReport(settings));
        }

        [Fact]
        public void PNGExportReport_ShouldThrowNotImplemented()
        {
            var strategy = new PNGExportReport();
            var settings = new ReportSettings();

            Assert.Throws<NotImplementedException>(() => strategy.ExportReport(settings));
        }
    }

    public class SprintReportTests
    {
        [Fact]
        public void ExportReport_WithoutStrategy_ShouldThrowNullReference()
        {
            var report = new SprintReport();
            var settings = new ReportSettings();

            Assert.Throws<NullReferenceException>(() => report.ExportReport(settings));
        }
    }

    public class FakeExport : IExportReport
    {
        public bool Called = false;

        public Document ExportReport(ReportSettings settings)
        {
            Called = true;
            return new Document();
        }


        [Fact]
        public void ExportReport_ShouldUseInjectedStrategy()
        {
            var report = new SprintReport();
            var fake = new FakeExport();

            report.SetExportStrategy(fake);

            var result = report.ExportReport(new ReportSettings());

            Assert.True(fake.Called);
            Assert.NotNull(result);
        }
    }
}
    

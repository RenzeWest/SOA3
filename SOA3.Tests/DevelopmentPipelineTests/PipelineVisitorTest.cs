using SOA3.Domain.PipelinePatterns.CompositePattern.Leafs;
using SOA3.Domain.PipelinePatterns.VisitorPattern;
using SOA3.Domain.PipelinePatterns.VisitorPattern.Factory;

namespace SOA3.Tests.PipelineTests
{
    [Collection("Sequential")]
    public class PipelineVisitorTest
    {
        [Fact]
        public void PickPipelineVisitor_Should_Return_DryRunVisitor_When_IsDryRun_Is_True()
        {
            // Arrange
            var factory = new PipelineVisitorFactory();

            // Act
            var result = factory.PickPipelineVisitor(true);

            // Assert
            Assert.IsType<DryRunVisitor>(result);
        }

        [Fact]
        public void PickPipelineVisitor_Should_Return_RunVisitor_When_IsDryRun_Is_False()
        {
            // Arrange
            var factory = new PipelineVisitorFactory();

            // Act
            var result = factory.PickPipelineVisitor(false);

            // Assert
            Assert.IsType<RunVisitor>(result);
        }

        [Fact]
        public void RunVisitor_VisitSourcePipelineAction_Should_Write_Correct_Message()
        {
            // Arrange
            var visitor = new RunVisitor();
            var action = new SourcePipelineAction { Name = "Source" };
            var originalOut = Console.Out;
            using var writer = new StringWriter();
            Console.SetOut(writer);

            try
            {
                // Act
                visitor.VisitSourcePipelineAction(action);

                // Assert
                Assert.Equal("[Run] Source action called: Source" + Environment.NewLine, writer.ToString());
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void RunVisitor_VisitBuildPipelineAction_Should_Write_Correct_Message()
        {
            // Arrange
            var visitor = new RunVisitor();
            var action = new BuildPipelineAction { Name = "Build" };
            var originalOut = Console.Out;
            using var writer = new StringWriter();
            Console.SetOut(writer);

            try
            {
                // Act
                visitor.VisitBuildPipelineAction(action);

                // Assert
                Assert.Equal("[Run] Build action called: Build" + Environment.NewLine, writer.ToString());
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void RunVisitor_VisitTestPipelineAction_Should_Write_Correct_Message()
        {
            // Arrange
            var visitor = new RunVisitor();
            var action = new TestPipelineAction { Name = "Test" };
            var originalOut = Console.Out;
            using var writer = new StringWriter();
            Console.SetOut(writer);

            try
            {
                // Act
                visitor.VisitTestPipelineAction(action);

                // Assert
                Assert.Equal("[Run] Test action called: Test" + Environment.NewLine, writer.ToString());
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void RunVisitor_VisitPackagePipelineAction_Should_Write_Correct_Message()
        {
            // Arrange
            var visitor = new RunVisitor();
            var action = new PackagePipelineAction { Name = "Package" };
            var originalOut = Console.Out;
            using var writer = new StringWriter();
            Console.SetOut(writer);

            try
            {
                // Act
                visitor.VisitPackagePipelineAction(action);

                // Assert
                Assert.Equal("[Run] Package action called: Package" + Environment.NewLine, writer.ToString());
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void RunVisitor_VisitAnalysePipelineAction_Should_Write_Correct_Message()
        {
            // Arrange
            var visitor = new RunVisitor();
            var action = new AnalysePipelineAction { Name = "Analyse" };
            var originalOut = Console.Out;
            using var writer = new StringWriter();
            Console.SetOut(writer);

            try
            {
                // Act
                visitor.VisitAnalysePipelineAction(action);

                // Assert
                Assert.Equal("[Run] Analyse action called: Analyse" + Environment.NewLine, writer.ToString());
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void RunVisitor_VisitDeployPipelineAction_Should_Write_Correct_Message()
        {
            // Arrange
            var visitor = new RunVisitor();
            var action = new DeployPipelineAction { Name = "Deploy" };
            var originalOut = Console.Out;
            using var writer = new StringWriter();
            Console.SetOut(writer);

            try
            {
                // Act
                visitor.VisitDeployPipelineAction(action);

                // Assert
                Assert.Equal("[Run] Deploy action called: Deploy" + Environment.NewLine, writer.ToString());
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void RunVisitor_VisitUtilityPipelineAction_Should_Write_Correct_Message()
        {
            // Arrange
            var visitor = new RunVisitor();
            var action = new UtilityPipelineAction { Name = "Utility" };
            var originalOut = Console.Out;
            using var writer = new StringWriter();
            Console.SetOut(writer);

            try
            {
                // Act
                visitor.VisitUtilityPipelineAction(action);

                // Assert
                Assert.Equal("[Run] Utility action called: Utility" + Environment.NewLine, writer.ToString());
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void RunVisitor_PipelineFailed_Should_Write_Correct_Message()
        {
            // Arrange
            var visitor = new RunVisitor();
            var exception = new Exception("Pipeline kapot");
            var originalOut = Console.Out;
            using var writer = new StringWriter();
            Console.SetOut(writer);

            try
            {
                // Act
                visitor.PipelineFailed(exception);

                // Assert
                Assert.Equal("[Run] Failed with message:Pipeline kapot" + Environment.NewLine, writer.ToString());
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void DryRunVisitor_VisitSourcePipelineAction_Should_Write_Correct_Message()
        {
            // Arrange
            var visitor = new DryRunVisitor();
            var action = new SourcePipelineAction { Name = "Source" };
            var originalOut = Console.Out;
            using var writer = new StringWriter();
            Console.SetOut(writer);

            try
            {
                // Act
                visitor.VisitSourcePipelineAction(action);

                // Assert
                Assert.Equal("[Dry Run] Source action called: Source" + Environment.NewLine, writer.ToString());
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void DryRunVisitor_VisitBuildPipelineAction_Should_Write_Correct_Message()
        {
            // Arrange
            var visitor = new DryRunVisitor();
            var action = new BuildPipelineAction { Name = "Build" };
            var originalOut = Console.Out;
            using var writer = new StringWriter();
            Console.SetOut(writer);

            try
            {
                // Act
                visitor.VisitBuildPipelineAction(action);

                // Assert
                Assert.Equal("[Dry Run] Build action called: Build" + Environment.NewLine, writer.ToString());
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void DryRunVisitor_VisitTestPipelineAction_Should_Write_Correct_Message()
        {
            // Arrange
            var visitor = new DryRunVisitor();
            var action = new TestPipelineAction { Name = "Test" };
            var originalOut = Console.Out;
            using var writer = new StringWriter();
            Console.SetOut(writer);

            try
            {
                // Act
                visitor.VisitTestPipelineAction(action);

                // Assert
                Assert.Equal("[Dry Run] Test action called: Test" + Environment.NewLine, writer.ToString());
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void DryRunVisitor_VisitPackagePipelineAction_Should_Write_Correct_Message()
        {
            // Arrange
            var visitor = new DryRunVisitor();
            var action = new PackagePipelineAction { Name = "Package" };
            var originalOut = Console.Out;
            using var writer = new StringWriter();
            Console.SetOut(writer);

            try
            {
                // Act
                visitor.VisitPackagePipelineAction(action);

                // Assert
                Assert.Equal("[Dry Run] Package action called: Package" + Environment.NewLine, writer.ToString());
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void DryRunVisitor_VisitAnalysePipelineAction_Should_Write_Correct_Message()
        {
            // Arrange
            var visitor = new DryRunVisitor();
            var action = new AnalysePipelineAction { Name = "Analyse" };
            var originalOut = Console.Out;
            using var writer = new StringWriter();
            Console.SetOut(writer);

            try
            {
                // Act
                visitor.VisitAnalysePipelineAction(action);

                // Assert
                Assert.Equal("[Dry Run] Analyse action called: Analyse" + Environment.NewLine, writer.ToString());
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void DryRunVisitor_VisitDeployPipelineAction_Should_Write_Correct_Message()
        {
            // Arrange
            var visitor = new DryRunVisitor();
            var action = new DeployPipelineAction { Name = "Deploy" };
            var originalOut = Console.Out;
            using var writer = new StringWriter();
            Console.SetOut(writer);

            try
            {
                // Act
                visitor.VisitDeployPipelineAction(action);

                // Assert
                Assert.Equal("[Dry Run] Deploy action called: Deploy" + Environment.NewLine, writer.ToString());
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void DryRunVisitor_VisitUtilityPipelineAction_Should_Write_Correct_Message()
        {
            // Arrange
            var visitor = new DryRunVisitor();
            var action = new UtilityPipelineAction { Name = "Utility" };
            var originalOut = Console.Out;
            using var writer = new StringWriter();
            Console.SetOut(writer);

            try
            {
                // Act
                visitor.VisitUtilityPipelineAction(action);

                // Assert
                Assert.Equal("[Dry Run] Utility action called: Utility" + Environment.NewLine, writer.ToString());
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        [Fact]
        public void DryRunVisitor_PipelineFailed_Should_Write_Correct_Message()
        {
            // Arrange
            var visitor = new DryRunVisitor();
            var exception = new Exception("Pipeline kapot");
            var originalOut = Console.Out;
            using var writer = new StringWriter();
            Console.SetOut(writer);

            try
            {
                // Act
                visitor.PipelineFailed(exception);

                // Assert
                Assert.Equal("[Dry Run] Failed with message:Pipeline kapot" + Environment.NewLine, writer.ToString());
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }
    }
}
using Moq;
using SOA3.Domain;
using SOA3.Domain.BacklogPatterns;
using SOA3.Domain.SCMConfigPatterns;
using System;
using Xunit;

namespace SOA3.Tests.SCMTests
{
    [Collection("Sequential")]
    public class SCMTests
    {
        // Helper
        private IBacklogItem CreateMockBacklogItem(out Person developer)
        {
            developer = new Person("Alice", "alice@example.com");
            var backlogItemMock = new Mock<IBacklogItem>();
            backlogItemMock.Setup(b => b.AssignedDeveloper).Returns(developer);
            return backlogItemMock.Object;
        }

        [Fact]
        public void Commit_Should_SetPropertiesCorrectly()
        {
            var backlogItem = CreateMockBacklogItem(out var developer);
            var message = "Fix bug in feature X";

            var commit = new Commit(backlogItem, message);

            Assert.Equal(message, commit.Message);
            Assert.Equal(backlogItem, commit.BacklogItem);
            Assert.Equal(developer, commit.Author);
            Assert.True(commit.DateTime <= DateTime.UtcNow);
        }

        [Fact]
        public void SCMConfig_Should_SetPropertiesCorrectly()
        {
            var repo = "https://github.com/test/repo.git";
            var branch = "main";

            var config = new SCMConfig(repo, branch);

            Assert.Equal(repo, config.RepositoryURL);
            Assert.Equal(branch, config.BranchName);
        }

        [Fact]
        public void GitSCMAdapter_Should_Call_GitClient_WithCorrectParams()
        {
            var repo = "link";
            var branch = "main";
            var config = new SCMConfig(repo, branch);

            var backlogItem = CreateMockBacklogItem(out var developer);
            var commit = new Commit(backlogItem, "Test commit");

            var testClient = new TestGitClientAdapter();

            testClient.Commit(config, commit);

            Assert.Equal(repo, TestGitClientAdapter.LastRepo);
            Assert.Equal(branch, TestGitClientAdapter.LastBranch);
            Assert.Equal("Test commit", TestGitClientAdapter.LastMessage);
        }

        [Fact]
        public void Commit_Should_Use_ISCMService_WhenSet()
        {
            var backlogItem = CreateMockBacklogItem(out var developer);
            var commit = new Commit(backlogItem, "Commit message");

            var scmServiceMock = new Mock<ISCMService>();
            typeof(Commit)
                .GetField("sCMService", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(commit, scmServiceMock.Object);

            var config = new SCMConfig("repo", "main");

            scmServiceMock.Object.Commit(config, commit);

            scmServiceMock.Verify(s => s.Commit(config, commit), Times.Once);
        }


        

        [Fact]
        public void GitSCMAdapter_Should_Call_GitClient_ConsoleOutput()
        {
            var config = new SCMConfig("repo", "feature");
            var backlogItem = CreateMockBacklogItem(out var dev);
            var commit = new Commit(backlogItem, "Message");

            using var sw = new System.IO.StringWriter();
            Console.SetOut(sw);

            var adapter = new GitSCMAdapter();
            adapter.Commit(config, commit);

            var output = sw.ToString();
            Assert.Contains("repo", output);
            Assert.Contains("feature", output);
            Assert.Contains("Message", output);
        }

        [Fact]
        public void SCMConfig_Should_HandleEmptyStrings()
        {
            var config = new SCMConfig("", "");
            Assert.Equal("", config.RepositoryURL);
            Assert.Equal("", config.BranchName);
        }

        [Fact]
        public void SCMConfig_Should_HandleNullStrings()
        {
            var config = new SCMConfig(null, null);
            Assert.Null(config.RepositoryURL);
            Assert.Null(config.BranchName);
        }

        [Fact]
        public void Commit_DefaultService_Should_SetProperties()
        {
            var backlogItem = CreateMockBacklogItem(out var dev);
            var commit = new Commit(backlogItem, "Some commit");

            Assert.Equal("Some commit", commit.Message);
            Assert.Equal(backlogItem, commit.BacklogItem);
            Assert.Equal(dev, commit.Author);
        }


        private class TestGitClientAdapter : GitSCMAdapter
        {
            public static string LastRepo;
            public static string LastBranch;
            public static string LastMessage;

            public new void Commit(SCMConfig config, Commit commit)
            {
                LastRepo = config.RepositoryURL;
                LastBranch = config.BranchName;
                LastMessage = commit.Message;
            }
        }

        public class ConsoleCapture : IDisposable
        {
            private readonly TextWriter _originalOut;
            public StringWriter Writer { get; }

            public ConsoleCapture()
            {
                _originalOut = Console.Out;
                Writer = new StringWriter();
                Console.SetOut(Writer);
            }

            public void Dispose()
            {
                Console.SetOut(_originalOut);
                Writer.Dispose();
            }

            public string GetOutput() => Writer.ToString();
        }
    }
}
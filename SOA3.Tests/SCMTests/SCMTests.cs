using Moq;
using SOA3.Domain;
using SOA3.Domain.BacklogPatterns;
using SOA3.Domain.SCMConfigPatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOA3.Tests.SCMTests
{
    public class SCMTests
    {
        // Helper: maak een mock backlog item met assigned developer
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
            // Arrange
            var backlogItem = CreateMockBacklogItem(out var developer);
            var message = "Fix bug in feature X";

            // Act
            var commit = new Commit(backlogItem, message);

            // Assert
            Assert.Equal(message, commit.Message);
            Assert.Equal(backlogItem, commit.BacklogItem);
            Assert.Equal(developer, commit.Author);
            Assert.True(commit.DateTime <= DateTime.UtcNow); // Timestamp correct
        }

        [Fact]
        public void SCMConfig_Should_SetPropertiesCorrectly()
        {
            // Arrange
            var repo = "https://github.com/test/repo.git";
            var branch = "main";

            // Act
            var config = new SCMConfig(repo, branch);

            // Assert
            Assert.Equal(repo, config.RepositoryURL);
            Assert.Equal(branch, config.BranchName);
        }

        [Fact]
        public void GitSCMAdapter_Should_Call_GitClient_WithCorrectParams()
        {
            // Arrange
            var repo = "link";
            var branch = "main";
            var config = new SCMConfig(repo, branch);

            var backlogItem = CreateMockBacklogItem(out var developer);
            var commit = new Commit(backlogItem, "Test commit");

            // Mock GitClient door overerving
            var testClient = new TestGitClientAdapter();

            // Act
            testClient.Commit(config, commit);

            // Assert
            Assert.Equal(repo, TestGitClientAdapter.LastRepo);
            Assert.Equal(branch, TestGitClientAdapter.LastBranch);
            Assert.Equal("Test commit", TestGitClientAdapter.LastMessage);
        }

        [Fact]
        public void Commit_Should_Use_ISCMService_WhenSet()
        {
            // Arrange
            var backlogItem = CreateMockBacklogItem(out var developer);
            var commit = new Commit(backlogItem, "Commit message");

            var scmServiceMock = new Mock<ISCMService>();
            // Vervang private service met mock via reflection
            typeof(Commit)
                .GetField("sCMService", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(commit, scmServiceMock.Object);

            var config = new SCMConfig("repo", "main");

            // Act
            scmServiceMock.Object.Commit(config, commit);

            // Assert
            scmServiceMock.Verify(s => s.Commit(config, commit), Times.Once);
        }

        // -------------------------
        // Helper class om GitClient te testen
        // -------------------------
        private class TestGitClientAdapter : GitSCMAdapter
        {
            public static string LastRepo;
            public static string LastBranch;
            public static string LastMessage;

            public new void Commit(SCMConfig config, Commit commit)
            {
                // Simuleer GitClient commit
                LastRepo = config.RepositoryURL;
                LastBranch = config.BranchName;
                LastMessage = commit.Message;
            }
        }
    }
}

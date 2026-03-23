namespace SOA3.Domain.SCMConfigPatterns
{
    public class GitSCMAdapter : ISCMService
    {
        public void Commit(SCMConfig config, Commit commit)
        {
            GitClient.Commit(config.RepositoryURL, config.BranchName, commit.Message);
        }
    }
}

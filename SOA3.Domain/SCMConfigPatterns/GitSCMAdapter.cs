namespace SOA3.Domain.SCMConfigPatterns
{
    public class GitSCMAdapter : ISCMService
    {
        public void Commit(SCMConfig config, string message)
        {
            GitClient.Commit(config.RepositoryURL, config.BranchName, message);
        }
    }
}

namespace SOA3.Domain.SCMConfigPatterns
{
    public class GitClient
    {
        public static void Commit(string repository, string branchName, string commitMessage)
        {
            Console.WriteLine($"Git is committing the code to {repository} on branch {branchName} with message {commitMessage}");
        }
    }
}

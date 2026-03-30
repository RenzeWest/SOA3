namespace SOA3.Domain.SCMConfigPatterns
{
    public class SCMConfig
    {
        private Guid _id =  new Guid();
        public string RepositoryURL { get; }
        public string BranchName { get; }

        public SCMConfig(string repositoryURL, string branchName)
        {
            RepositoryURL = repositoryURL;
            BranchName = branchName;
        }
    }
}

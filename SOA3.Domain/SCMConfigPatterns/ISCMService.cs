namespace SOA3.Domain.SCMConfigPatterns
{
    public interface ISCMService
    {
        void Commit(SCMConfig config, string message);
    }
}

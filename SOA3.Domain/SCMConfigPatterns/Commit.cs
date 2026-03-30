using SOA3.Domain.BacklogPatterns;

namespace SOA3.Domain.SCMConfigPatterns
{
    public class Commit
    {
        private Guid _id = Guid.NewGuid();
        public DateTime DateTime { get; } = DateTime.UtcNow;
        public string Message { get; }
        public Person Author { get; }
        public IBacklogItem BacklogItem { get; }
        private ISCMService sCMService = new GitSCMAdapter();

        public Commit(IBacklogItem backlogItem, string message)
        {
            Message = message;
            BacklogItem = backlogItem;
            Author = backlogItem.AssignedDeveloper;

            // TODO: Push to github. So implement logic
            //sCMService.Commit(new SCMConfig() {/* IDK */ },this);
            
        }
    }
}

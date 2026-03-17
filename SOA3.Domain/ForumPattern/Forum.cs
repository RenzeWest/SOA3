namespace SOA3.Domain.ForumPattern
{
    public class Forum
    {
        private List<DiscussionThread> _discussionThreads = new List<DiscussionThread>();

        public void AddDiscussionThread(DiscussionThread thread) => _discussionThreads.Add(thread);
        
    }
}

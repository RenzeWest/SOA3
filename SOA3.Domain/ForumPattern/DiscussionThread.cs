using SOA3.Domain.BacklogPatterns;
using System.Text.Json;

namespace SOA3.Domain.ForumPattern
{
    public class DiscussionThread
    {
        private Person _creator;
        private string _description;
        private BacklogItem _relatedBacklogItem;
        private List<Comment> _mainCommentThread = new List<Comment>();
        
        public void AddComment(Comment comment)
        {
            if (_relatedBacklogItem.GetState() is not DoneState)
            {
                _mainCommentThread.Add(comment);
            }
        }
        public void RemoveComment(Comment comment) => _mainCommentThread.Remove(comment);
        public List<Comment> GetAllComments() => _mainCommentThread;  
    }
}

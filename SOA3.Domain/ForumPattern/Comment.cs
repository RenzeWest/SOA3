using SOA3.Domain.NotificationPatterns;

namespace SOA3.Domain.ForumPattern
{
    public class Comment
    {
        private NotificationPublisher _publisher = new NotificationPublisher();

        private string _comment;
        public Person _author {get; private set;}
        private DateTime _UTCPostedAt;
        private List<Comment> _replies = new List<Comment>();

        public Comment(string comment, Person author)
        {
            _UTCPostedAt = DateTime.UtcNow;
            _comment = comment;
            _author = author;
        }

        public void AddComment(Comment comment) 
        { 
            _replies.Add(comment);
            var message = $"You have recieved a comment from {comment._author.GetName()}on ${_comment}.";
            _publisher.NotifySubscribers(new Notification(DateTime.UtcNow, message));
        } 
        public void RemoveComment(Comment comment) => _replies.Remove(comment);
        public List<Comment> GetComments() => _replies;

        public override string? ToString()
        {
            return $"{_UTCPostedAt.ToString()} - {_author.GetName}: {_comment}";
        }
    }
}